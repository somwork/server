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
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private IConfiguration config;
        private IUserRepository repo;

        public SecurityController(IConfiguration config, IUserRepository repo)
        {
            this.config = config;
            this.repo = repo;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateToken([FromBody]LoginModel login)
        {
            ActionResult response = Unauthorized();
            User user = await Authenticate(login);

            if (user == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            string tokenString = BuildToken(user);
            return response = Ok(new { token = tokenString });
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest(); // 400 Bad request 
            }

            // generate a random salt 
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);

            // generate the salted and hashed password 
            var sha = SHA256.Create();
            var saltedPassword = user.Password + saltText;
            var saltedhashedPassword = Convert.ToBase64String(
            sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));

            user.Salt = saltText;
            user.Password = saltedhashedPassword;

            User added = await repo.CreateAsync(user);
            return CreatedAtRoute("GetUser", // use named route
            new { Id = added.Id }, user); // 201 Created
        }

        private async Task<User> Authenticate(LoginModel loginModel)
        {
            IEnumerable<User> potentialUsers = (await repo.RetrieveAllAsync())
                .Where(user => user.Username == loginModel.Username);

            if (potentialUsers == null)
            {
                return null;
            }

            foreach (User u in potentialUsers)
            {
                // re-generate the salted and hashed password 
                var sha = SHA256.Create();
                var saltedPassword = loginModel.Password + u.Salt;
                var saltedhashedPassword = Convert.ToBase64String(
                sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));

                if (saltedhashedPassword.Equals(u.Password))
                {
                    return u;
                }
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

        //private async Task<User> Authenticate(LoginModel login)
        //{
        //    string result = await CheckPassword(login);
        //    if (result.Equals(""))
        //    {
        //        return null;
        //    }
        //    login.Password = result;
        //    return await repo.RetrieveSpecificAsync(login);
        //}
    }
}
