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
        public IActionResult Get()
        {
            return new ObjectResult(repo.RetrieveAll());
        }

        // GET: api/locations/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Location l = repo.Retrieve(Id);
            if (l == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(l); // 200 ok
        }

        // POST: api/locations
        [HttpPost]
        public IActionResult Create([FromBody]Location location)
        {
            if (location == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "CreateLocation: location is null" });
            }

            Location added = repo.Create(location);

            return new ObjectResult(added);
        }

        // PUT: api/locations/[id]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Location l)
        {
            if (l == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Location existing = repo.Retrieve(l.Id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            repo.Update(l);
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/locations/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Location existing = repo.Retrieve(Id);
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
