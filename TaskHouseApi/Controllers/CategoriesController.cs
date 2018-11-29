using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Controllers.CRUDController;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriesController : CRUDController<Category>
    {
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
