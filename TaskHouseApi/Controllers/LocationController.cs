using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Security;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private ILocationRepository repo;

        // constructor injects registered repository 
        public LocationsController(ILocationRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/locations/
        [HttpGet]
        public async Task<IEnumerable<Location>> Get()
        {
            return await repo.RetrieveAll();
        }

        // GET: api/locations/[id]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int Id)
        {
            Location l = await repo.Retrieve(Id);
            if(l == null)
            {
                return NotFound(); // 404 Resource not found 
            }

            return new ObjectResult(l); // 200 ok
        }

        // POST: api/locations
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Location location)
        {
            if(location == null)
            {
                // 400 Bad request 
                return BadRequest(new { error = "CreateLocation: location is null" });
            }

            Location added = await repo.Create(location);

            return new ObjectResult(added); 
        }

        // PUT: api/locations/[id]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] Location l)
        {
            if(l == null || l.Id != Id )
            {
                return BadRequest(); // 400 Bad request
            }

            Location existing = await repo.Retrieve(Id);

            if( existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            await repo.Update(Id, l);
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/locations/[id]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            Location existing = await repo.Retrieve(Id);
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
