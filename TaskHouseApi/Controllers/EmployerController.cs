using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Controllers;
using TaskHouseApi.Repositories;

namespace Controllers{


         [Authorize]
         [Route("api/[controller]")]
    public class EmployerController : UsersController{

           private IUserRepository repo;

        public EmployerController(IUserRepository repo) : base(repo)
        {
            this.repo = repo;
        }

        // GET: api/Employer/[id]
        [HttpGet("{id}")]
        public async Task<IEnumerable<Tasks>> GetAllTask()
        {
            Employer e = await UsersController.repo.Retrieve(id);
            return e.Tasks;
        }

        // GET: api/sk/[id]/[TaskId]
        [HttpGet("{id}"),("{TaskId}")]
        public async Task<IActionResult> GetSpecificTask(int Id, int TaskId)
        {   
          Employer e = repo.Retrieve(Id);
          foreach (var task in e.Tasks)
          {
              if (TaskId==task.Id)
              {
                  return task;
              }
          }


}
}