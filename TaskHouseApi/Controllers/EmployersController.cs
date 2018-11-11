using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EmployersController : Controller
    {
        private IPasswordService passwordService;

        private IUserRepository repo;

        // constructor injects registered repository
        public EmployersController(IUserRepository repo, IPasswordService passwordService)
        {
            this.repo = repo;
            this.passwordService = passwordService;
        }

        // GET: api/employers/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Employer u = repo.Retrieve(Id) as Employer;
            if (u == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(u); // 200 OK
        }

        // GET: api/empolyers/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(repo.RetrieveAll());
        }

        // POST: api/employers
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody]Employer employer)
        {
            if (employer == null)
            {
                return BadRequest(new { error = "CreateEmployer: empolyer is null" }); // 400 Bad request
            }

            Employer existingEmployer = (repo.RetrieveAll()).SingleOrDefault(e => e.Username == employer.Username) as Employer;

            if (existingEmployer != null)
            {
                return BadRequest(new { error = "Username in use" });
            }

            var hashResult = passwordService.GenerateNewPassword(employer);

            employer.Salt = hashResult.saltText;
            employer.Password = hashResult.saltechashedPassword;

            Employer added = repo.Create(employer) as Employer;

            return new ObjectResult(added);
        }

        // PUT: api/employers/[id]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Employer e)
        {
            if (e == null)
            {
                return BadRequest(); // 400 Bad request
            }

            if (!repo.isInDatabase(e.Id))
            {
                return NotFound();
            }

            repo.Update(e);
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/employers/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var existing = repo.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            bool deleted = repo.Delete(Id);

            if (!deleted)
            {
                return BadRequest();
            }

            return new NoContentResult(); // 204 No content
        }
    }
}
