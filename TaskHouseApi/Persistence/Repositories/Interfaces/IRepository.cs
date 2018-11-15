using System.Collections.Generic;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        T Retrieve(int Id);
        IEnumerable<T> RetrieveAll();

        void Create(T baseModel);

        void Update(T baseModel);

        void Delete(T baseModel);
        void Delete(int id);
    }
}
