using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TaskHouseApi.Model;
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

            task.MapUrgencyFactor();

            unitOfWork.Tasks.Create(task);
            unitOfWork.Save();

            return new ObjectResult(task); //200 ok
        }

        [Authorize(Roles = "TaskHouseApi.Model.Employer")]
        [HttpPut("{id}/complete")]
        public IActionResult CompleteTask(int Id)
        {
            //Get the given task from the id parameter
            Task task = unitOfWork.Tasks.Retrieve(Id);

            //if the task doesn't exist return badrequest
            if (task == null)
            {
                return BadRequest(new { error = "Task doesn't exist" }); //400 bad request
            }

            //Bad request if task is already completed
            if (task.Completed == true)
            {
                return BadRequest(new { error = "Task already completed" });
            }

            task.Completed = true;

            unitOfWork.Tasks.Update(task);
            unitOfWork.Save();

            return new ObjectResult(task);
        }

        // POST: api/tasks/[id]
        [Authorize(Roles = "TaskHouseApi.Model.Worker")]
        [HttpPost("{id}/estimate")]
        public IActionResult CreateEstimate(int Id, [FromBody] Estimate estimate)
        {
            //Get the given task from the id parameter
            Task task = unitOfWork.Tasks.Retrieve(Id);

            //if the task doesn't exist return badrequest
            if (task == null)
            {
                return BadRequest(new { error = "Task doesn't exist" }); //400 bad request
            }

            // Add the estimates for the specific task id to the collection of estimates
            task.Estimates = unitOfWork.Estimates.RetrieveAllEstimatesForSpecificTaskId(Id).ToList();

            //if the collection consists of 0 estimates then the average estimate should be set to 0
            if (task.Estimates.Count == 0)
            {
                task.AverageEstimate = 0;
            }

            //if the estimate from the body is null return bad request
            if (estimate == null)
            {
                return BadRequest(new { error = "CreateEstimate: estimate is null" }); //400 bad request
            }

            if (!TryValidateModel(estimate))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            int currentUserId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault
            (
                c => c.Type == ClaimTypes.NameIdentifier).Value
            );

            //add the current estimate to the collection because it is not saved in the database yet
            task.Estimates.Add(estimate);

            //calculate the average estimate and set the average estimate for the given task
            task.AverageEstimate = task.CalculateAverageEstimate();

            estimate.WorkerId = currentUserId;
            estimate.TaskId = task.Id;

            //update the given tasks average estimate
            unitOfWork.Tasks.Update(task);

            //create estimate in the tabel Estimates in the database
            unitOfWork.Estimates.Create(estimate);

            //save the changes in the database
            unitOfWork.Save();

            return new ObjectResult(estimate); //200 ok
        }

        [Authorize]
        [HttpPost("{id}/messages")]
        public IActionResult CreateMessage(int Id, [FromBody] Message message)
        {
            //Get the given task from the id parameter
            Task task = unitOfWork.Tasks.Retrieve(Id);

            //if the task doesn't exist return badrequest
            if (task == null)
            {
                return BadRequest(new { error = "Task doesn't exist" }); //400 bad request
            }
            task.Messages = unitOfWork.Messages.RetrieveAllMessagesForSpecificTaskId(Id).ToList();

            //if the message from the body is null return bad request
            if (message == null)
            {
                return BadRequest(new { error = "CreateMessage: message is null" }); //400 bad request
            }

            if (!TryValidateModel(message))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            int currentUserId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault
            (
                c => c.Type == ClaimTypes.NameIdentifier).Value
            );

            message.UserId = currentUserId;
            message.TaskId = task.Id;

            task.Messages.Add(message);

            unitOfWork.Tasks.Update(task);

            unitOfWork.Messages.Create(message);

            unitOfWork.Save();

            return new ObjectResult(message); //200 ok

        }

        [HttpGet("{id}/messages")]
        public IActionResult GetMessages(int Id)
        {
            return new ObjectResult(unitOfWork.Messages.RetrieveAllMessagesForSpecificTaskId(Id));
        }


        [Authorize(Roles = "TaskHouseApi.Model.Employer")]
        [HttpGet("{Id}/estimates")]
        public IActionResult GetEstimates(int Id)
        {
            return new ObjectResult(
                unitOfWork.Estimates.RetrieveAllEstimatesForSpecificTaskId(Id)
            );
        }

        [Authorize(Roles = "TaskHouseApi.Model.Employer")]
        [HttpPut("{Id}/categories")]
        public IActionResult AddCategory(int Id, int categoryId)
        {
            if (
                !unitOfWork.Tasks.isInDatabase(Id) ||
                !unitOfWork.Categories.isInDatabase(categoryId) ||
                !unitOfWork.Tasks.AddCategory(Id, categoryId))
            {
                return BadRequest();
            }

            unitOfWork.Save();
            return new OkResult();
        }

        [HttpGet("{Id}/categories")]
        public IActionResult GetCategories(int Id)
        {
            if (
                !unitOfWork.Tasks.isInDatabase(Id))
            {
                return BadRequest();
            }

            var categories = unitOfWork.Categories.GetCategoriesForTask(Id);
            return new OkObjectResult(categories);
        }
    }
}

