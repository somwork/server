using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Controllers.CRUDController;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EmployersController : CRUDController<Employer>
    {
        private IPasswordService passwordService;

        public EmployersController(IUnitOfWork unitOfWork, IPasswordService passwordService) : base(unitOfWork)
        {
            this.passwordService = passwordService;
        }

        [NonAction]
        public override IActionResult Create([FromBody] Employer baseModel)
        {
            return BadRequest();
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
    }
}
