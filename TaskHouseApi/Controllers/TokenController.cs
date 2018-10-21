using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskHouseApi.Logic;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration config;
        private IUserRepository repo;

        public TokenController(IConfiguration config, IUserRepository repo)
        {
            this.config = config;
            this.repo = repo;
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> CreateToken([FromBody]LoginModel login)
        {
            ActionResult response = Unauthorized();
            User user = await Authenticate(login);

            if (user == null)
            {
                return NotFound(new { error = "CreateTask: user is null" }); // 404 Resource not found 
            }

            string tokenString = BuildToken(user);
            return response = Ok(new { token = tokenString });
        }

        private async Task<User> Authenticate(LoginModel loginModel)
        {
            User potentialUser = (await repo.RetrieveAllAsync())
                .SingleOrDefault(user => user.Username.Equals(loginModel.Username));

            if (potentialUser == null)
            {
                return null;
            }

            if (SecurityHandler.GenerateSaltedHashedPassword(loginModel.Password, potentialUser.Salt).Equals(potentialUser.Password))
            {
                return potentialUser;
            }

            return null;
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
