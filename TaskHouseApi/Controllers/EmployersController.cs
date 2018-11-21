using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EmployersController : Controller
    {
        private IPasswordService passwordService;

        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public EmployersController(IUnitOfWork unitOfWork, IPasswordService passwordService)
        {
            this.unitOfWork = unitOfWork;
            this.passwordService = passwordService;
        }

        // GET: api/employers/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Employer u = unitOfWork.Employers.Retrieve(Id);
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
            return new ObjectResult(unitOfWork.Employers.RetrieveAll());
        }

        // POST: api/employers
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(string password, [FromBody]Employer employer)
        {
            if (employer == null)
            {
                return BadRequest(new { error = "CreateEmployer: empolyer is null" }); // 400 Bad request
            }

            employer.Password = password;
            ModelState.Clear();

            if (!TryValidateModel(employer))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            User existingUser = (unitOfWork.Users.RetrieveAll()).SingleOrDefault(e => e.Username == employer.Username);

            if (existingUser != null)
            {
                return BadRequest(new { error = "Username is in use" });
            }

            var hashResult = passwordService.GenerateNewPassword(employer);

            employer.Salt = hashResult.saltText;
            employer.Password = hashResult.saltechashedPassword;

            unitOfWork.Employers.Create(employer);
            unitOfWork.Save();

            return new ObjectResult(employer);
        }

        // PUT: api/employers/[id]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employer e)
        {
            if (e == null)
            {
                return BadRequest(); // 400 Bad request
            }

            if (!unitOfWork.Employers.isInDatabase(id))
            {
                return NotFound();
            }

            e.Id = id;
            unitOfWork.Employers.Update(e);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/employers/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var existing = unitOfWork.Employers.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Employers.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
