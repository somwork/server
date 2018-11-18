using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategorysController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public CategorysController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/category/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Categorys.RetrieveAll());
        }

        // GET: api/category/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Category l = unitOfWork.Categorys.Retrieve(Id);
            if (l == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(l); // 200 ok
        }

        // POST: api/category
        [HttpPost]
        public IActionResult Create([FromBody]Category category)
        {
            if (category == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "Create category: category is null" });
            }

            unitOfWork.Categorys.Create(category);
            unitOfWork.Save();

            return new ObjectResult(category);
        }

        // PUT: api/category/[id]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Category existing = unitOfWork.Categorys.Retrieve(id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            category.Id = id;
            unitOfWork.Categorys.Update(category);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/category/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Category existing = unitOfWork.Categorys.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Categorys.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
