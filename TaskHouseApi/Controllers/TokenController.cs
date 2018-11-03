using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Repositories;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration config;
        private IUserRepository repo;
        private IPasswordService passwordService;
        private ITokenService tokenService;

        public TokenController(IConfiguration config, IUserRepository repo, IPasswordService passwordService, ITokenService tokenService)
        {
            this.config = config;
            this.repo = repo;
            this.passwordService = passwordService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]LoginModel login)
        {
            ActionResult response = Unauthorized();
            User user = await Authenticate(login);

            if (user == null)
            {
                return response;
            }

            // Sets the claims for the token
            var usersClaims = new[]
            {
                // User Id
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            string tokenString = tokenService.GenerateAccessToken(usersClaims);
            string refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add
            (
                new RefreshToken { Token = refreshToken }
            );

            var result = await repo.UpdateAsync(user.Id, user);
            if (result == null)
            {
                return StatusCode(500);
            }

            return new ObjectResult(new
            {
                accessToken = tokenString,
                refreshToken = refreshToken
            });
        }

        [HttpPut]
        public async Task<IActionResult> Refresh([FromBody]RefreshModel refreshModel)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(refreshModel.AccessToken);
            
            // If null, the token isn't valid 
            if (principal == null)
            {
                return BadRequest();
            }

            // Gets the user Id from Claims
            int Id;
            Int32.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out Id);

            User user = await repo.RetrieveAsync(Id);

            // Checks that the user exists and has a refresh token
            if (user == null || user.RefreshTokens.Count() == 0)
            {
                return BadRequest();
            }

            // Checks that the refresh token from the client,
            // matches one asignt to the user
            RefreshToken storedRefreshToken = null;
            foreach (RefreshToken r in user.RefreshTokens)
            {
                if ((r.Token).Equals(refreshModel.RefreshToken))
                {
                    storedRefreshToken = r;
                }
            }

            if (storedRefreshToken == null)
            {
                return BadRequest();
            }

            // Generates a new access token and refresh token
            var newJwtToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            // Deletes the old refresh token from database
            bool res = repo.DeleteRefrechToken(user, storedRefreshToken);
            if (res == false)
            {
                return StatusCode(500);
            }

            // Add the new refresh token to user
            user.RefreshTokens.Add
                (
                    new RefreshToken { Token = newRefreshToken }
                );

            var result = await repo.UpdateAsync(Id, user);
            if (result == null)
            {
                return StatusCode(500);
            }

            return new ObjectResult(new
            {
                accessToken = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

        private async Task<User> Authenticate(LoginModel loginModel)
        {
            User potentialUser = (await repo.RetrieveAllAsync())
                .SingleOrDefault(user => user.Username.Equals(loginModel.Username));

            if (potentialUser == null)
            {
                return null;
            }

            bool isAuthenticated = passwordService
                .GenerateSaltedHashedPassword(loginModel.Password, potentialUser.Salt)
                .Equals(potentialUser.Password);

            if (!isAuthenticated)
            {
                return null;
            }

            return potentialUser;
        }
    }
}
