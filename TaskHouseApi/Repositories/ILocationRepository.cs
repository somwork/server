using System;
using System.Collections.Generic;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface ILocationRepository
    {
        Location Create(Location l);

        IEnumerable<Location> RetrieveAll();

        Location Retrieve(int Id);

        Location Update(Location l);

        bool Delete(int Id);
    }
}
