using System.Collections.Generic;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.Repositories.Interfaces
{
    public interface IEstimateRepository : IRepository<Estimate>
    {
        //Custom functionality to be added
        IEnumerable<Estimate> RetrieveAllEstimatesForSpecificTaskId(int Id);
    }
}
