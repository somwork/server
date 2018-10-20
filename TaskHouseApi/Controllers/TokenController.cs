using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Controllers
{
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateToken([FromBody]LoginModel login)
        {
            ActionResult response = Unauthorized();
            User user = await Authenticate(login);

            if (user != null)
            {
                string tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
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

        private async Task<User> Authenticate(LoginModel login)
        {
            return await repo.RetrieveSpecificAsync(login);
        }
    }
}
