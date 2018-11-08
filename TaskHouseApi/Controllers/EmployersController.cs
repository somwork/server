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
    [Authorize]
    [Route("api/[controller]")]
    public class EmployersController : Controller
    {
        private IEmployersRepository repo;

        // constructor injects registered repository 
        public EmployersController(IEmployersRepository repo)
        {
            this.repo = repo;
        }


        // GET: api/empolyers/[id] 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            Employer u = await repo.Retrieve(Id);
            if (u == null)
            {
                return NotFound(); // 404 Resource not found 
            }
            return new ObjectResult(u); // 200 OK 
        }

        // GET: api/empolyers/
        [HttpGet]
        public async Task<IEnumerable<Employer>> Get()
        {
            return await repo.RetrieveAll();
        }

        // POST: api/employers
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Employer employer)
        {
            if (employer == null)
            {
                return BadRequest(new { error = "CreateEmployer: empolyer is null" }); // 400 Bad request 
            }

            Employer existingEmployer = (await repo.RetrieveAll()).SingleOrDefault(e => e.Username == employer.Username);

            if (existingEmployer != null) {
                return BadRequest(new { error = "Username in use" });
            }

            var hashResult = SecurityHandler.GenerateNewPassword(employer);

            employer.Salt = hashResult.saltText;
            employer.Password = hashResult.saltechashedPassword;

            Employer added = await repo.Create(employer);
            
            return new ObjectResult(added);

            
        }

        // PUT: api/employers/[id] 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] Employer e)
        {
            if (e == null || e.Id != Id)
            {
                return BadRequest(); // 400 Bad request 
            }

            var existing = await repo.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            await repo.Update(Id, e);
            return new NoContentResult(); // 204 No content 
        }


        // DELETE: api/employers/[id] 
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
