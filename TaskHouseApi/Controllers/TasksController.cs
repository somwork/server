using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Get(int Id)
        {
            Task t = repo.Retrieve(Id);
            if (t == null)
            {
                return NotFound(); //404 resource not found
            }
            return new ObjectResult(t); //200 ok
        }

        //GET: api/tasks
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(repo.RetrieveAll());
        }

        // POST: api/tasks
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest(new { error = "CreateTask: task is null" }); //400 bad request
            }

            Task added = repo.Create(task);

            return new ObjectResult(added); //200 ok
        }

        //PUT: api/tasks/[id]
        [HttpPut("{id}")]

        public IActionResult Update([FromBody] Task t)
        {
            if (t == null)
            {
                return BadRequest(); //400 bad request
            }

            Task existing = repo.Retrieve(t.Id);

            if (existing == null)
            {
                return NotFound(); //404 resource not found
            }

            repo.Update(t);
            return new NoContentResult();  //204 no content
        }

        //DELETE: api/users/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Task existing = repo.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); //404 resource not found
            }

            bool deleted = repo.Delete(Id);

            if (!deleted)
            {
                return BadRequest(); //400 bad request
            }

            return new NoContentResult(); //204 No content
        }
    }
}
