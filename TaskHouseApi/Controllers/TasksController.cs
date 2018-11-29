using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using System.Security.Claims;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Controllers.CRUDController;

namespace TaskHouseApi.Controllers
{
    //base address: api/tasks
    [Authorize]
    [Route("api/[controller]")]
    public class TasksController : CRUDController<Task>
    {
        public TasksController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        // POST: api/tasks
        [Authorize(Roles = "TaskHouseApi.Model.Employer")]
        [HttpPost]
        public override IActionResult Create([FromBody] Task task)
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
    }
}
