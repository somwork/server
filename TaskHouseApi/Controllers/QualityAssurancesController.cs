using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Controllers.CRUDController;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class QualityAssurancesController : CRUDController<QualityAssurance>
    {
        private IPasswordService passwordService;

        public QualityAssurancesController(IUnitOfWork unitOfWork, IPasswordService passwordService) : base(unitOfWork)
        {
            this.passwordService = passwordService;
        }

        [NonAction]
        public override IActionResult Create([FromBody] QualityAssurance baseModel)
        {
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(string password, [FromBody]QualityAssurance qa)
        {
            if (qa == null)
            {
                return BadRequest(new { error = "Create QA: QA is null" }); // 400 Bad request
            }

            qa.Password = password;
            ModelState.Clear();

            if (!TryValidateModel(qa))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            User existingUser = (unitOfWork.Users.RetrieveAll()).SingleOrDefault(w => w.Username == qa.Username);

            if (existingUser != null)
            {
                return BadRequest(new { error = "Username is in use" }); // 400 Bad request
            }

            var hashResult = passwordService.GenerateNewPassword(qa);

            qa.Salt = hashResult.saltText;
            qa.Password = hashResult.saltechashedPassword;

            unitOfWork.QualityAssurances.Create(qa);
            unitOfWork.Save();

            return new ObjectResult(qa);
        }
    }
}
