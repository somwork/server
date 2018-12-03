using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Controllers.CRUDController;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class QualityAssurancesController : UserController<QualityAssurance>
    {
        public QualityAssurancesController(IUnitOfWork unitOfWork, IPasswordService passwordService) : base(unitOfWork, passwordService) { }
    }
}
