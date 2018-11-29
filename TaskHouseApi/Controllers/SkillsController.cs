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

        [Authorize(Roles = "TaskHouseApi.Model.Worker")]
        [HttpPost]
        public override IActionResult Create([FromBody] Skill skill)
        {
            var createResult = CreateBasicCheck(skill);
            if (createResult != null)
            {
                return createResult;
            }

            skill.WorkerId = GetCurrentUserId();

            unitOfWork.Skills.Create(skill);
            unitOfWork.Save();

            return new ObjectResult(skill); //200 ok
        }
    }
}
