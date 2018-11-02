using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Security;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Controllers
{
    // base address: api/customers 
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class WorkersController : Controller
    {
        private IWorkerRepository repo;

        // constructor injects registered repository 
        public WorkersController(IWorkerRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/users 
        // GET: api/users/?username=[username] 
        [HttpGet]
        public async Task<IEnumerable<Worker>> Get(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return await repo.RetrieveAll();
            }
            
            return (await repo.RetrieveAll())
                .Where(worker => worker.Username == username);
        }

        // GET: api/users/[id] 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            Worker w = await repo.Retrieve(Id);
            if (w == null)
            {
                return NotFound(); // 404 Resource not found 
            }
            return new ObjectResult(w); // 200 OK 
        }

        // POST: api/users
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody]Worker worker)
        {
            if (worker == null)
            {
                return BadRequest(new { error = "CreateUser: user is null" }); // 400 Bad request 
            }

            Worker existingWorker = (await repo.RetrieveAll()).SingleOrDefault(w => w.Username == worker.Username);

            if (existingWorker != null) {
                return BadRequest(new { error = "Username in use" });
            }

            var hashResult = SecurityHandler.GenerateNewPassword(worker);

            worker.Salt = hashResult.saltText;
            worker.Password = hashResult.saltechashedPassword;

            await repo.Create(worker);
            
            return Ok();

        
        }

        // PUT: api/users/[id] 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] Worker w)
        {
            if (w == null || w.Id != Id)
            {
                return BadRequest(); // 400 Bad request 
            }

            var existing = await repo.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            await repo.Update(Id, w);
            return new NoContentResult(); // 204 No content 
        }

        // DELETE: api/users/[id] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var existing = await repo.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            bool deleted = await repo.Delete(Id);

            if (!deleted)
            {
                return BadRequest(); 
            }

            return new NoContentResult(); // 204 No content
        }
    }
}
