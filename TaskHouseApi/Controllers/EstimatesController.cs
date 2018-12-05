using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    //base address: api/estimates
    [Authorize]
    [Route("api/[controller]")]
    public class EstimatesController : Controller
    {
        private IUnitOfWork unitOfWork;

        public EstimatesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //GET: api/estimates
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Estimates.RetrieveAll());
        }

        //GET: api/estimates/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Estimate e = unitOfWork.Estimates.Retrieve(id);
            if (e == null)
            {
                return NotFound(); //404 resource not found
            }
            return new ObjectResult(e); //200 ok
        }

        [Authorize(Roles = "TaskHouseApi.Model.Employer")]
        [HttpPut("{Id}/accept")]
        public IActionResult AcceptEstimate(int Id)
        {
            //Get the given estimate from the id parameter
            Estimate estimate = unitOfWork.Estimates.Retrieve(Id);

            //if the estimate doesn't exist return badrequest
            if (estimate == null)
            {
              return BadRequest(new { error = "Estimate doesn't exist" }); //400 bad request
            }

            estimate.Accepted = true;
            unitOfWork.Estimates.Update(estimate);
            unitOfWork.Save();

            return new NoContentResult();
        }

        //DELETE: api/estimates/[id]
        [Authorize(Roles = "TaskHouseApi.Model.Worker")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Estimate estimate = unitOfWork.Estimates.Retrieve(id);

            if (estimate == null)
            {
                return NotFound(); //404 resource not found
            }

            unitOfWork.Estimates.Delete(id);
            unitOfWork.Save();

            return new NoContentResult(); //204 No content
        }
    }
}
