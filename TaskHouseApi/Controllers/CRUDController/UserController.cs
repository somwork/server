using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers.CRUDController
{
    public abstract class UserController<U> : CRUDController<U>, IUserController<U> where U : User
    {
        protected internal IPasswordService passwordService;
        public UserController(IUnitOfWork unitOfWork, IPasswordService passwordService) : base(unitOfWork)
        {
            this.passwordService = passwordService;
        }

        [NonAction]
        public override IActionResult Create([FromBody] U baseModel)
        {
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(string password, [FromBody]U user)
        {
            if (user == null)
            {
                return BadRequest(new { error = "Createuser: user is null" }); // 400 Bad request
            }

            user.Password = password;
            ModelState.Clear();

            if (!TryValidateModel(user))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            User existingUser = (unitOfWork.Users.RetrieveAll()).SingleOrDefault(w => w.Username == user.Username);

            if (existingUser != null)
            {
                return BadRequest(new { error = "Username is in use" }); // 400 Bad request
            }

            var hashResult = passwordService.GenerateNewPassword(user);

            user.Salt = hashResult.saltText;
            user.Password = hashResult.saltechashedPassword;

            unitOfWork.Repository<U>().Create(user);
            unitOfWork.Save();

            return new ObjectResult(user);
        }
    }
}
