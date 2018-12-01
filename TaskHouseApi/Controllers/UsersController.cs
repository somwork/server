using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUnitOfWork unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            User u = unitOfWork.Users.Retrieve(Id);
            if (u == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(u); // 200 ok
        }
    }
}
