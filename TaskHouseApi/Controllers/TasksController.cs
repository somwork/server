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
            var createResult = CreateBasicCheck(task);
            if (createResult != null)
            {
                return createResult;
            }

            task.EmployerId = GetCurrentUserId();

            unitOfWork.Tasks.Create(task);
            unitOfWork.Save();

            return new ObjectResult(task); //200 ok
        }
    }
}
