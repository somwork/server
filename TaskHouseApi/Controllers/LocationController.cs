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
    public class LocationsController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public LocationsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/locations/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Locations.RetrieveAll());
        }

        // GET: api/locations/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Location l = unitOfWork.Locations.Retrieve(Id);
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

            unitOfWork.Locations.Create(location);
            unitOfWork.Save();

            return new ObjectResult(location);
        }

        // PUT: api/locations/[id]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Location l)
        {
            if (l == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Location existing = unitOfWork.Locations.Retrieve(l.Id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            unitOfWork.Locations.Update(l);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/locations/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Location existing = unitOfWork.Locations.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Locations.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
