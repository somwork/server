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
    public class WorkersController : UserController<Worker>
    {
        public WorkersController(IUnitOfWork unitOfWork, IPasswordService passwordService) : base(unitOfWork, passwordService) { }

        [HttpGet("{id}/skills")]
        public IActionResult GetSkillsForWorker(int Id)
        {
            return new ObjectResult(unitOfWork.Skills.GetSkillsForWorker(Id));
        }

        [Authorize(Roles = "TaskHouseApi.Model.Worker")]
        [HttpGet("{id}/tasks/accepted")]
        public IActionResult GetAcceptedTasksForWorker(int id)
        {
            return new ObjectResult(unitOfWork.Tasks.GetAcceptedTasksForWorker(id));
        }

        [Authorize(Roles = "TaskHouseApi.Model.Worker")]
        [HttpGet("{id}/tasks/estimated")]
        public IActionResult GetEstimatedTasksForWorker(int id)
        {
            return new ObjectResult(unitOfWork.Tasks.GetEstimatedTasksForWorker(id));
        }

        [Authorize(Roles = "TaskHouseApi.Model.Worker")]
        [HttpGet("tasks")]
        public IActionResult GetAvailableTasksForWorker()
        {
            return new ObjectResult(unitOfWork.Tasks.GetAvailableTasksForWorker());
        }
    }
}
