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
    public class WorkersController : Controller
    {
        //var
        private IWorkerRepository repo;

        //constructor
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
        public async Task<IEnumerable<Worker>> GetAll()
        {
            IEnumerable<Worker> wl = await repo.RetrieveAll();
            return wl; 
        }

        
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
                return BadRequest(new { error = "Username in use" }); // 400 Bad request 
            }

            var hashResult = SecurityHandler.GenerateNewPassword(worker);

            worker.Salt = hashResult.saltText;
            worker.Password = hashResult.saltechashedPassword;

            await repo.Create(worker);
            
            return Ok() ; // 200 ok

        
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Worker>> Update(int Id, [FromBody] Worker w)
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

            return   await repo.Update(Id, w);
      
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var existing = await repo.Retrieve(Id);
            if (existing == null)
            {
                return BadRequest();  // 400 Bad request 
            }

            bool deleted = await repo.Delete(Id);

            if (deleted=false)
            {
                return BadRequest();  // 400 Bad request 
            }

            return new NotFoundResult();
        }
    }
}
