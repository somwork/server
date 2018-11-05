using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    // base address: api/customers 
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserRepository repo;
        private IPasswordService passwordService;

        // constructor injects registered repository 
        public UsersController(IUserRepository repo, IPasswordService passwordService)
        {
            this.repo = repo;
            this.passwordService = passwordService;
        }

        // GET: api/users 
        // GET: api/users/?username=[username] 
        [HttpGet]
        public async Task<IEnumerable<User>> Get(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return await repo.RetrieveAllAsync();
            }
            
            return (await repo.RetrieveAllAsync())
                .Where(user => user.Username == username);
        }

        // GET: api/users/[id]
        [Authorize(Roles = "User")] 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            User u = await repo.RetrieveAsync(Id);
            if (u == null)
            {
                return NotFound(); // 404 Resource not found 
            }
            return new ObjectResult(u); // 200 OK 
        }

        // POST: api/users
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest(new { error = "CreateUser: user is null" }); // 400 Bad request 
            }

            User existingUser = (await repo.RetrieveAllAsync()).SingleOrDefault(u => u.Username == user.Username);

            if (existingUser != null) {
                return BadRequest(new { error = "Username in use" });
            }

            var hashResult = passwordService.GenerateNewPassword(user);

            user.Salt = hashResult.saltText;
            user.Password = hashResult.saltechashedPassword;

            User added = await repo.CreateAsync(user);
            
            return Ok();

            //return CreatedAtRoute("GetUser", // use named route
            //new { Id = added.Id }, user); // 201 Created
        }

        // PUT: api/users/[id] 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] User u)
        {
            if (u == null || u.Id != Id)
            {
                return BadRequest(); // 400 Bad request 
            }

            var existing = await repo.RetrieveAsync(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            await repo.UpdateAsync(Id, u);
            return new NoContentResult(); // 204 No content 
        }

        // DELETE: api/users/[id] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var existing = await repo.RetrieveAsync(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            bool deleted = await repo.DeleteAsync(Id);

            if (!deleted)
            {
                return BadRequest(); 
            }

            return new NoContentResult(); // 204 No content
        }
    }
}
