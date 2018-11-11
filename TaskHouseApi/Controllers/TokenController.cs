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
        private ITokenService tokenService;
        private IAuthService authService;
        private IUserRepository userRepository;

        public TokenController(IConfiguration config, ITokenService tokenService, IAuthService authService, IUserRepository userRepository)
        {
            this.config = config;
            this.tokenService = tokenService;
            this.authService = authService;
            this.userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult Create([FromBody]LoginModel login)
        {
            // Check's if user is autherised
            User user = authService.Authenticate(login);
            if (user == null)
            {
                return Unauthorized();
            }

            // Finds the role of the user
            string role = findRoleName(user);
            if (role == null)
            {
                return StatusCode(500);
            }

            // Sets the claims for the token
            var usersClaims = new[]
            {
                // User Id
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            string tokenString = tokenService.GenerateAccessToken(usersClaims);
            string refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add
            (
                new RefreshToken { Token = refreshToken }
            );

            var result = userRepository.Update(user);
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
        public IActionResult Refresh([FromBody]RefreshModel refreshModel)
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

            User user = userRepository.Retrieve(Id);

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
            bool res = userRepository.DeleteRefrechToken(storedRefreshToken);
            if (res == false)
            {
                return StatusCode(500);
            }

            // Add the new refresh token to user
            user.RefreshTokens.Add
            (
                new RefreshToken { Token = newRefreshToken }
            );

            var result = userRepository.Update(user);
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

        private string findRoleName(User user)
        {
            if (user is Worker)
            {
                return nameof(Worker);
            }

            if (user is Employer)
            {
                return nameof(Employer);
            }

            return null;
        }
    }
}
