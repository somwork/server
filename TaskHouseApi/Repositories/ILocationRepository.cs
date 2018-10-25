using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface ILocationRepository
    {

        Task<Location> Create(Location l);

        Task<IEnumerable<Location>> RetrieveAll();

        Task<Location> Retrieve(int Id);

        Task<Location> Update(int Id, Location l);

        Task<bool> Delete(int Id);
    }
}
