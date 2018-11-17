
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OffersController : Controller
    {
        private IUnitOfWork unitOfWork;

        // constructor injects registered repository
        public OffersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/offers/
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(unitOfWork.Offers.RetrieveAll());
        }

        // GET: api/offers/[id]
        [HttpGet("{id}")]
        public IActionResult Get(int Id)
        {
            Offer l = unitOfWork.Offers.Retrieve(Id);
            if (l == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(l); // 200 ok
        }

        // POST: api/offers
        [HttpPost]
        public IActionResult Create([FromBody]Offer offer)
        {
            if (offer == null)
            {
                // 400 Bad request
                return BadRequest(new { error = "Createoffer: offer is null" });
            }

            unitOfWork.Offers.Create(offer);
            unitOfWork.Save();

            return new ObjectResult(offer);
        }

        // PUT: api/offers/[id]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Offer offer)
        {
            if (offer == null)
            {
                return BadRequest(); // 400 Bad request
            }

            Offer existing = unitOfWork.Offers.Retrieve(id);

            if (existing == null)
            {
                return NotFound(); // 404 resource not found
            }

            offer.Id = id;
            unitOfWork.Offers.Update(offer);
            unitOfWork.Save();
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/offers/[id]
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            Offer existing = unitOfWork.Offers.Retrieve(Id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            unitOfWork.Offers.Delete(Id);
            unitOfWork.Save();

            return new NoContentResult(); // 204 No content
        }
    }
}
