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
        void UpdatePart(T baseModel, string[] nameOfPropertysToIgnore);

        void Delete(T baseModel);
        void Delete(int Id);
        bool isInDatabase(int Id);
    }
}
