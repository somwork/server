using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Controllers.CRUDController;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReferencesController : CRUDController<Reference>
    {
        public ReferencesController(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
