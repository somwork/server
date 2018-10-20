using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Controllers
{
    // base address: api/customers 
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserRepository repo;

        // constructor injects registered repository 
        public UsersController(IUserRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/users 
        // GET: api/users/?username=[username] 
        [HttpGet]
        public async Task<IEnumerable<User>> GetCustomers(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return await repo.RetrieveAllAsync();
            }
            else
            {
                return (await repo.RetrieveAllAsync())
                .Where(user => user.Username == username);
            }
        }

        // GET: api/users/[id] 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int Id)
        {
            User u = await repo.RetrieveAsync(Id);
            if (u == null)
            {
                return NotFound(); // 404 Resource not found 
            }
            return new ObjectResult(u); // 200 OK 
        }

        // POST: api/users 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User u)
        {
            if (u == null)
            {
                return BadRequest(); // 400 Bad request 
            }
            User added = await repo.CreateAsync(u);
            return CreatedAtRoute("GetUser", // use named route
            new { Id = added.Id }, u); // 201 Created 
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

        // DELETE: api/user/[id] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var existing = await repo.RetrieveAsync(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            bool deleted = await repo.DeleteAsync(Id);

            if (deleted)
            {
                return new NoContentResult(); // 204 No content 
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
