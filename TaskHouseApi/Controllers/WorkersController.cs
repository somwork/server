namespace TaskHouseApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TaskHouseApi.Model;
    using TaskHouseApi.Repositories;
    using TaskHouseApi.Security;

    [Authorize]
    [Route("api/[controller]")]
    public class WorkersController : Controller
    {
        private IWorkerRepository repo;

        public WorkersController(IWorkerRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            Worker w = await repo.Retrieve(Id);
            if (w == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(w);
        }

        [HttpGet]
        public async Task<IEnumerable<Worker>> Get()
        {
            IEnumerable<Worker> wl = await repo.RetrieveAll();
            return wl;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Worker worker)
        {
            if (worker == null)
            {
                return BadRequest(new { error = "CreateWorker: worker is null" }); // 400 Bad request
            }

            Worker existingWorker = (await repo.RetrieveAll()).SingleOrDefault(w => w.Username == worker.Username);

            if (existingWorker != null) {
                return BadRequest(new { error = "Username in worker" }); // 400 Bad request
            }

            var hashResult = SecurityHandler.GenerateNewPassword(worker);

            worker.Salt = hashResult.saltText;
            worker.Password = hashResult.saltechashedPassword;

            await repo.Create(worker);

            return Ok() ; // 200 ok
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] Worker w)
        {
            if (w == null || w.Id != Id)
            {
                return BadRequest(); // 400 Bad request
            }

            Worker existing = await repo.Retrieve(Id);

            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            await repo.Update(Id, w);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var existing = await repo.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            bool deleted = await repo.Delete(Id);

            if (deleted==false)
            {
                return BadRequest(); 
            }

            return new  NoContentResult();
        }
    }
}
