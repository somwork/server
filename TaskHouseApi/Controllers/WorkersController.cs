using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Controllers;
using TaskHouseApi.Repositories;

namespace Controllers{
  
    // base address: api/customers
    [Authorize]
    [Route("api/[controller]")]
    public class WorkersController : UsersController
    {
         private IUserRepository repo;

        public WorkersController(IUserRepository repo) : base(repo)
        {
            this.repo=repo;
        }
    }

}