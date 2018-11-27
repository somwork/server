using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BudgetsController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public BudgetsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/budgets/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Budgets.RetrieveAll());
        }

        // GET: api/budgets/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Budget l = unitOfWork.Budgets.Retrieve(Id);
            if (l == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(l); // 200 ok
        }

        // POST: api/budgets
        [HttpPost]
        public IActionResult Create([FromBody]Budget budgets)
        {
            if (budgets == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "Create budgets: budgets is null" });
            }

            if (!TryValidateModel(budgets))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            unitOfWork.Budgets.Create(budgets);
            unitOfWork.Save();

            return new ObjectResult(budgets);
        }

        // PUT: api/budgets/[id]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Budget budgets)
        {
            if (budgets == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Budget existing = unitOfWork.Budgets.Retrieve(id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            budgets.Id = id;
            unitOfWork.Budgets.Update(budgets);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/budgets/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Budget existing = unitOfWork.Budgets.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Budgets.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
