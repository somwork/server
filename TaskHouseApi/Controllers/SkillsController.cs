using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public SkillsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/skills/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Skills.RetrieveAll());
        }

        // GET: api/skills/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Skill s = unitOfWork.Skills.Retrieve(Id);
            if (s == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(s); // 200 ok
        }

        // POST: api/skills
        [HttpPost]
        public IActionResult Create([FromBody]Skill skill)
        {
            if (skill == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "CreateLocation: skill is null" });
            }

            if (!TryValidateModel(skill))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            unitOfWork.Skills.Create(skill);
            unitOfWork.Save();

            return new ObjectResult(skill);
        }

        // PUT: api/skills/[id]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Skill s)
        {
            if (s == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Skill existing = unitOfWork.Skills.Retrieve(id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            s.Id = id;
            unitOfWork.Skills.Update(s);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/skills/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Skill existing = unitOfWork.Skills.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Skills.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
