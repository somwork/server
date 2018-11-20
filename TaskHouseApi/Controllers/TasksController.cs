using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using System.Security.Claims;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    //base address: api/tasks
    [Authorize]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registrered repository
        public TasksController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //GET: api/tasks/id
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Task t = unitOfWork.Tasks.Retrieve(Id);
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
            return new ObjectResult(unitOfWork.Tasks.RetrieveAll());
        }

        // POST: api/tasks
        [Authorize(Roles = "TaskHouseApi.Model.Employer")]
        [HttpPost]
        public IActionResult Create([FromBody] Task task)
        {
            if (task == null)
            {
                return BadRequest(new { error = "CreateTask: task is null" }); //400 bad request
            }

            if (!TryValidateModel(task))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            int currentUserId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault
            (
                c => c.Type == ClaimTypes.NameIdentifier).Value
            );

            task.EmployerId = currentUserId;

            unitOfWork.Tasks.Create(task);
            unitOfWork.Save();

            return new ObjectResult(task); //200 ok
        }

        //PUT: api/tasks/[id]
        [HttpPut("{id}")]

        public IActionResult Update(int id, [FromBody] Task t)
        {
            if (t == null)
            {
                return BadRequest(); //400 bad request
            }

            Task existing = unitOfWork.Tasks.Retrieve(id);

            if (existing == null)
            {
                return NotFound(); //404 resource not found
            }

            t.Id = id;
            unitOfWork.Tasks.Update(t);
            unitOfWork.Save();
            return new NoContentResult();  //204 no content
        }

        //DELETE: api/users/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Task existing = unitOfWork.Tasks.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); //404 resource not found
            }

            unitOfWork.Tasks.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); //204 No content
        }
    }
}
