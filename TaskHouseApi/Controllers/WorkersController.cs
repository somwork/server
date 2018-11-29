using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaskHouseApi.Controllers.CRUDController;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkersController : CRUDController<Worker>
    {
        private IPasswordService passwordService;

        public WorkersController(IUnitOfWork unitOfWork, IPasswordService passwordService) : base(unitOfWork)
        {
            this.passwordService = passwordService;
        }


        [NonAction]
        public override IActionResult Create([FromBody] Worker baseModel)
        {
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(string password, [FromBody]Worker worker)
        {
            if (worker == null)
            {
                return BadRequest(new { error = "CreateWorker: worker is null" }); // 400 Bad request
            }

            worker.Password = password;
            ModelState.Clear();

            if (!TryValidateModel(worker))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            User existingUser = (unitOfWork.Users.RetrieveAll()).SingleOrDefault(w => w.Username == worker.Username);

            if (existingUser != null)
            {
                return BadRequest(new { error = "Username is in use" }); // 400 Bad request
            }

            var hashResult = passwordService.GenerateNewPassword(worker);

            worker.Salt = hashResult.saltText;
            worker.Password = hashResult.saltechashedPassword;

            unitOfWork.Workers.Create(worker);
            unitOfWork.Save();

            return new ObjectResult(worker);
        }
    }
}
