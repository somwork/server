using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Controllers.CRUDController;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EmployersController : UserController<Employer>
    {
        public EmployersController(IUnitOfWork unitOfWork, IPasswordService passwordService) : base(unitOfWork, passwordService) { }

        [HttpGet("{id}/tasks")]
        public IActionResult GetTasksForEmployer(int Id)
        {
            return new ObjectResult(unitOfWork.Tasks.GetTasksForEmployer(Id));
        }

        [Authorize(Roles = "TaskHouseApi.Model.Employer")]
        [HttpGet("{id}/tasks/accepted")]
        public IActionResult GetAcceptedTasksForEmployer(int id)
        {
            return new ObjectResult(unitOfWork.Tasks.GetAcceptedTasksForEmployer(id));
        }
    }
}
