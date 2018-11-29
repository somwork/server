using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Controllers.CRUDController;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SkillsController : CRUDController<Skill>
    {
        public SkillsController(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
