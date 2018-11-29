using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers.CRUDController
{
    public class CRUDController<T> : Controller, ICRUDController<T> where T : BaseModel
    {
        protected internal IUnitOfWork unitOfWork;

        public CRUDController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Repository<T>().RetrieveAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            T t = unitOfWork.Repository<T>().Retrieve(Id);
            if (t == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(t); // 200 ok
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] T baseModel)
        {
            if (baseModel == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "Create baseModel: baseModel is null" });
            }

            if (!TryValidateModel(baseModel))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            unitOfWork.Repository<T>().Create(baseModel);
            unitOfWork.Save();

            return new ObjectResult(baseModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] T baseModel)
        {
            if (baseModel == null)
            {
                return BadRequest(); // 400 Bad request
            }

            if (!unitOfWork.Repository<T>().isInDatabase(id))
            {
                return NotFound(); // 404 resource not found
            }

            baseModel.Id = id;
            unitOfWork.Repository<T>().UpdatePart(baseModel);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            T existing = unitOfWork.Repository<T>().Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Repository<T>().Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
