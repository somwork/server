using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Skill>> Get()
        {
            return await repo.RetrieveAll();
        }

        // GET: api/skills/[id]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            Skill s = await repo.Retrieve(Id);
            if(s == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            return new ObjectResult(s); // 200 ok
        }

        // POST: api/skills
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Skill skill)
        {
            if(skill == null)
            {
                // 400 Bad request 
                return BadRequest(new { error = "CreateLocation: skill is null" });
            }

            Skill added = await repo.Create(skill);

            return new ObjectResult(added); 
        }

        // PUT: api/skills/[id]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] Skill s)
        {
            if(s == null || s.Id != Id )
            {
                return BadRequest(); // 400 Bad request
            }

            Skill existing = await repo.Retrieve(Id);

            if( existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            await repo.Update(Id, s);
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/skills/[id]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            Skill existing = await repo.Retrieve(Id);
            if(existing == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            bool deleted = await repo.Delete(Id);

            if(!deleted)
            {
                return BadRequest();
            }

            return new NoContentResult(); // 204 No content
        }

    }
}
