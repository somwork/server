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

    //base address: api/tasks
    [Authorize]
    [Route("api/[controller]")]
    public class TasksController : Controller 
    { 
        private ITaskRepository repo; 

        // constructor injects registrered repository
        public TasksController(ITaskRepository repo) 
        { 
            this.repo = repo;
        }

        //GET: api/tasks/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id) 
        { 
            TaskHouseApi.Model.Task t = await repo.Retrieve(Id);
            if(t == null)
            {
                return NotFound(); //404 resource not found
            }
            return new ObjectResult(t); //200 ok

        }

        //GET: api/tasks
        [HttpGet]
        public async Task<IEnumerable<TaskHouseApi.Model.Task>> Get() 
        { 
            return await repo.RetrieveAll(); 
        }

        // POST: api/tasks
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TaskHouseApi.Model.Task task) 
        { 
            if (task == null)
            { 
                return BadRequest(new { error = "CreateTask: task is null"}); //400 bad request
            }

            TaskHouseApi.Model.Task added = await repo.Create(task);

            return Ok(); //200 ok
        }


        //PUT: api/tasks/[id]
        [HttpPut("{id}")]

        public async Task<IActionResult> Update (int Id, [FromBody] TaskHouseApi.Model.Task t) 
        { 
            if (t == null || t.Id != Id) 
            { 
                return BadRequest(); //400 bad request
            }

            TaskHouseApi.Model.Task existing = await repo.Retrieve(Id);

            if (existing == null)
            {  
                return NotFound(); //404 resource not found
            }

            await repo.Update(Id, t);
            return new NoContentResult();  //204 no content
        }


        //DELETE: api/users/[id]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int Id) 
        { 
            TaskHouseApi.Model.Task existing = await repo.Retrieve(Id);
            if(existing == null)
            {
                return NotFound(); //404 resource not found
            }

            bool deleted = await repo.Delete(Id);

            if(!deleted)
            { 
                return BadRequest(); //400 bad request
            }

            return new NoContentResult(); //204 No content
        }
        
    } 
}