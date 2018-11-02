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
        public async Task<ActionResult<string>> Create([FromBody]LoginModel login)
        {
            ActionResult response = Unauthorized();
            User user = await Authenticate(login);

            if (user == null)
            {
                return response;
            }

            var usersClaims = new[]
            {
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
                token = tokenString,
                refreshToken = refreshToken
            });
        }

        [HttpPost]
        public async Task<IActionResult> Refresh(string token, string refreshToken)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(token);
            int Id;
            Int32.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out Id);

            User user = await repo.RetrieveAsync(Id);

            if (user == null || user.RefreshTokens.Count() == 0)
            {
                return BadRequest();
            }

            RefreshToken storedRefreshToken = null;
            foreach (RefreshToken r in user.RefreshTokens)
            {
                if (r.Equals(refreshToken))
                {
                    storedRefreshToken = r;
                }
            }

            if (storedRefreshToken == null)
            {
                return BadRequest();
            }

            var newJwtToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();

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
                token = newJwtToken,
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

        /*private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }*/
    }
}
