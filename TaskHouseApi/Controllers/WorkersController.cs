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
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class WorkersController : Controller 
    {
        private IUserRepository repo;

        public WorkersController(IUserRepository repo)
        {
            this.repo = repo;  
        }

        [HttpGet("{UserId}/educations")]
        public async Task<IActionResult> GetEducations(int UserId)
        {
            Worker w = (Worker) await repo.RetrieveAsync(UserId);
            return new ObjectResult(w);
        }
    }
}