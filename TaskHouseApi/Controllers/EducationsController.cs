using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EducationsController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public EducationsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Education/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Educations.RetrieveAll());
        }

        // GET: api/Education/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Education l = unitOfWork.Educations.Retrieve(Id);
            if (l == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(l); // 200 ok
        }

        // POST: api/Education
        [HttpPost]
        public IActionResult Create([FromBody]Education education)
        {
            if (education == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "Create education: education is null" });
            }

            unitOfWork.Educations.Create(education);
            unitOfWork.Save();

            return new ObjectResult(education);
        }

        // PUT: api/Education/[id]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Education education)
        {
            if (education == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Education existing = unitOfWork.Educations.Retrieve(id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            education.Id = id;
            unitOfWork.Educations.Update(education);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/Education/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Education existing = unitOfWork.Educations.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Educations.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
