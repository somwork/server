using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;


namespace TaskHouseApi.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        //cache the users in a thread-safe dictionary to improve performance
        private static ConcurrentDictionary<int, Location> locationCache;

        //reference to database context
        private PostgresContext db;

        public LocationRepository(PostgresContext db)
        {
            this.db = db;

            //populates locationCache
            if(locationCache == null)
            {
                locationCache = new ConcurrentDictionary<int, Location>(
                    db.Locations.ToDictionary(c => c.Id)
             	);
            }
        }

        public async Task<Location> Create(Location l)
        {
            //add location to database using EF core
            await db.Locations.AddAsync(l);

            int affected = await db.SaveChangesAsync();

            //user not created
            if(affected != 1)
            {
                return null;
            }

            //if the location is new add it to cache, else call UpdateCache method
            return locationCache.AddOrUpdate(l.Id, l, UpdateCache);
        }

        public async Task<bool> Delete(int Id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                Location l = db.Locations.Find(Id);
                db.Locations.Remove(l);

                int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return null;
                }
                return System.Threading.Tasks.Task.Run(() => locationCache.TryRemove(Id, out l));
            });
        }

        public async Task<Location> Retrieve(int Id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                Location l;
                locationCache.TryGetValue(Id, out l);
                return l;
            });
        }

        public async Task<IEnumerable<Location>> RetrieveAll()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<Location>>(
                () => locationCache.Values
			); 
        }

        public async Task<Location> Update(int Id, Location l)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                db.Locations.Update(l);
                int affected = db.SaveChanges();

                if(affected != 1)
                {
                    return null;
                }

                return System.Threading.Tasks.Task.Run(() => UpdateCache(Id, l));
            });
        }

        private Location UpdateCache(int Id, Location l)
        {
            Location old;
            if(locationCache.TryGetValue(Id, out old))
            {
                if(locationCache.TryUpdate(Id, l, old))
                {
                    return l;
                }
            }
            return null;
        }
    }
}
