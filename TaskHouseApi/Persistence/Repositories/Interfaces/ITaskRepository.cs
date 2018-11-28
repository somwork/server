using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        void UpdatePart(Task basemodel);
    }
}
