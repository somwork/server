using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        private ISkillRepository repo;

        // constructor injects registered repository
        public SkillsController(ISkillRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/skills/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(repo.RetrieveAll());
        }

        // GET: api/skills/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Skill s = repo.Retrieve(Id);
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

            Skill added = repo.Create(skill);

            return new ObjectResult(added);
        }

        // PUT: api/skills/[id]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Skill s)
        {
            if (s == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Skill existing = repo.Retrieve(s.Id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            repo.Update(s);
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/skills/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Skill existing = repo.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            bool deleted = repo.Delete(Id);

            if (!deleted)
            {
                return BadRequest();
            }

            return new NoContentResult(); // 204 No content
        }
    }
}
