using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskHouseUnitTests
{
    public class FakeLocationRepository : ILocationRepository
    {
        private static List<Location> locationCache;

        public FakeLocationRepository()
        {
            locationCache = new List<Location>()
            {
                new Location()
                {
                    Id = 1,
                    Country = "Country1",
                    City = "City1",
                    ZipCode = "ZipCode1",
                    PrimaryLine = "PrimaryLine1",
                    SecondaryLine = "SecondaryLine1",
                },
                new Location()
                {
                    Id = 2,
                    Country = "Country2",
                    City = "City2",
                    ZipCode = "ZipCode2",
                    PrimaryLine = "PrimaryLine2",
                    SecondaryLine = "SecondaryLine2",
                },
                new Location()
                {
                    Id = 3,
                    Country = "Country3",
                    City = "City3",
                    ZipCode = "ZipCode3",
                    PrimaryLine = "PrimaryLine3",
                    SecondaryLine = "SecondaryLine3",
                },
            };
        }

        public async Task<Location> Create(Location l)
        {
            locationCache.Add(l);
            return l;
        }

        public async Task<Location> Retrieve(int Id)
        {
            return locationCache.Where(l => l.Id == Id).SingleOrDefault();
        }

        public async Task<IEnumerable<Location> > RetrieveAll()
        {
            return locationCache;
        }

        public async Task<Location> Update(int Id, Location location)
        {
            Location old = locationCache.Where(l => l.Id == Id).SingleOrDefault();
            int index = locationCache.IndexOf(old);

            locationCache[index] = location;
            return location;
        }

        public async Task<bool> Delete(int Id)
        {
            Location location = locationCache.Where(l => l.Id == Id).SingleOrDefault();

            if(location == null)
            {
                return false;
            }

            locationCache.Remove(location);
            return true;
        }

    }
}
