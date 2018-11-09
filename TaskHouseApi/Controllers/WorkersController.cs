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
    [Authorize]
    [Route("api/[controller]")]
    public class WorkersController : Controller
    {
        private IWorkerRepository repo;
        private IPasswordService passwordService;

        public WorkersController(IWorkerRepository repo, IPasswordService passwordService)
        {
            this.repo = repo;
            this.passwordService = passwordService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Worker w = repo.Retrieve(Id);
            if (w == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(w);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(repo.RetrieveAll());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody]Worker worker)
        {
            if (worker == null)
            {
                return BadRequest(new { error = "CreateWorker: worker is null" }); // 400 Bad request
            }

            Worker existingWorker = (repo.RetrieveAll()).SingleOrDefault(w => w.Username == worker.Username);

            if (existingWorker != null)
            {
                return BadRequest(new { error = "Username in worker" }); // 400 Bad request
            }

            var hashResult = passwordService.GenerateNewPassword(worker);

            worker.Salt = hashResult.saltText;
            worker.Password = hashResult.saltechashedPassword;

            Worker added = repo.Create(worker);

            return new ObjectResult(added);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Worker w)
        {
            if (w == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Worker existing = repo.Retrieve(w.Id);

            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            repo.Update(w);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var existing = repo.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            bool deleted = repo.Delete(Id);

            if (deleted == false)
            {
                return BadRequest();
            }

            return new NoContentResult();
        }
    }
}
