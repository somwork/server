using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReferencesController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public ReferencesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/reference/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.References.RetrieveAll());
        }

        // GET: api/reference/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Reference r = unitOfWork.References.Retrieve(Id);
            if (r == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(r); // 200 ok
        }

        // POST: api/reference
        [HttpPost]
        public IActionResult Create([FromBody]Reference reference)
        {
            if (reference == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "Create reference: reference is null" });
            }

            if (!TryValidateModel(reference))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            unitOfWork.References.Create(reference);
            unitOfWork.Save();

            return new ObjectResult(reference);
        }

        // PUT: api/reference/[id]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Reference reference)
        {
            if (reference == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Reference existing = unitOfWork.References.Retrieve(id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            reference.Id = id;
            unitOfWork.References.Update(reference);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/reference/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Reference existing = unitOfWork.References.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.References.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
