using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using System.Linq;

namespace TaskHouseUnitTests
{
    public class FakeRepository<T> : IRepository<T> where T : BaseModel
    {
        protected internal List<T> list;

        public FakeRepository(List<T> list)
        {
            this.list = list;
        }

        public void Create(T baseModel)
        {
            list.Add(baseModel);
        }

        public void Delete(T baseModel)
        {
            list.Remove(baseModel);
        }

        public void Delete(int Id)
        {
            T temp = Retrieve(Id);
            list.Remove(temp);
        }

        public T Retrieve(int Id)
        {
            return list.Where(t => t.Id == Id).SingleOrDefault();
        }

        public virtual IEnumerable<T> RetrieveAll()
        {
            return list;
        }

        public void Update(T baseModel)
        {
            T old = Retrieve(baseModel.Id);
            int index = list.IndexOf(old);

            list[index] = baseModel;
        }

        public void UpdatePart(T baseModel, string[] nameOfPropertysToIgnore)
        {
            throw new System.NotImplementedException();
        }
    }
}
