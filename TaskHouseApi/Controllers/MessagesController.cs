using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public MessagesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/message/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Messages.RetrieveAll());
        }

        // GET: api/message/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Message l = unitOfWork.Messages.Retrieve(Id);
            if (l == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(l); // 200 ok
        }

        // POST: api/message
        [HttpPost]
        public IActionResult Create([FromBody]Message message)
        {
            if (message == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "Create message: message is null" });
            }

            if (!TryValidateModel(message))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            unitOfWork.Messages.Create(message);
            unitOfWork.Save();

            return new ObjectResult(message);
        }

        // PUT: api/message/[id]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Message message)
        {
            if (message == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Message existing = unitOfWork.Messages.Retrieve(id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            message.Id = id;
            unitOfWork.Messages.Update(message);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/message/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Message existing = unitOfWork.Messages.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Messages.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
