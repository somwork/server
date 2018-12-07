using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;

namespace TaskHouseApi.Controllers.CRUDController
{
    public interface IUserController<U> : ICRUDController<U> where U : User
    {
        IActionResult Create([FromBody]CreateUserModel<U> inputUserModel);
    }
}
