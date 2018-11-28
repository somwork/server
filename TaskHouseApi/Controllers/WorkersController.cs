using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkersController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IPasswordService passwordService;

        public WorkersController(IUnitOfWork unitOfWork, IPasswordService passwordService)
        {
            this.unitOfWork = unitOfWork;
            this.passwordService = passwordService;
        }

        [HttpGet("{id}")]
        //[JsonNoNull]
        public IActionResult Get(int Id)
        {
            Worker w = unitOfWork.Workers.Retrieve(Id);
            if (w == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(w);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Workers.RetrieveAll());
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Worker w)
        {
            if (w == null)
            {
                return BadRequest(); // 400 Bad request
            }

            if (!unitOfWork.Workers.isInDatabase(id))
            {
                return NotFound();
            }

            w.Id = id;
            unitOfWork.Workers.UpdatePart(w);
            unitOfWork.Save();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var existing = unitOfWork.Workers.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Workers.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult();
        }
    }
}
