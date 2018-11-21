using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;

namespace TaskHouseApi.Controllers
{
    [Route("api/[controller]")]
    public class CurrenciesController : Controller
    {
        private CurrencyRESTService service = new CurrencyRESTService();

        public async Task<ActionResult> Index()
        {
            Currency c = await service.GetCurrenciesAsync();

            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }

            return new ObjectResult(c);
        }
    }
}
