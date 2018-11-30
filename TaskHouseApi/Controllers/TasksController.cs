using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TaskHouseApi.Model;
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

        //GET: api/tasks/
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

        // POST: api/tasks/[id]
        [Authorize(Roles = "TaskHouseApi.Model.Worker")]
        [HttpPost("{id}/estimate")]
        public IActionResult Create(int Id, [FromBody] Estimate estimate)
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
            if(task.Estimates.Count == 0)
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
    }
}
