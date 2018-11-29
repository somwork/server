using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;

namespace TaskHouseApi.Controllers.CRUDController
{
    public interface ICRUDController<T> where T : BaseModel
    {
        IActionResult Get();
        IActionResult Get(int Id);
        IActionResult Create([FromBody]T baseModel);
        IActionResult Update(int id, [FromBody] T baseModel);
        IActionResult Delete(int Id);
    }
}
