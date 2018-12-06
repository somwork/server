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
        public IActionResult Create([FromBody]CreateUserModel<U> inputUserModel)
        {

            if (
                inputUserModel == null ||
                inputUserModel.User == null)
            {
                return BadRequest(new { error = "Input: is null" }); // 400 Bad request
            }

            U user = inputUserModel.User;
            user.Password = inputUserModel.Password;
            ModelState.Clear();

            if (!TryValidateModel(inputUserModel))
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

            unitOfWork.Repository<U>().Create((U)user);
            unitOfWork.Save();

            return new ObjectResult(user);
        }
    }
}
