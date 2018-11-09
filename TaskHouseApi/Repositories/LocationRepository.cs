using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        //reference to database context
        private PostgresContext db;

        public LocationRepository(PostgresContext db)
        {
            this.db = db;
        }

        public Location Create(Location l)
        {
            //add location to database using EF core
            db.Locations.Add(l);

            int affected = db.SaveChanges();

            //user not created
            if (affected != 1)
            {
                return null;
            }
            return l;
        }

        public bool Delete(int Id)
        {
            Location l = db.Locations.Find(Id);
            db.Locations.Remove(l);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return false;
            }
            return true;
        }

        public Location Retrieve(int Id)
        {
            return db.Locations
                .Where(user => user.Id == Id)
                .SingleOrDefault();
        }

        public IEnumerable<Location> RetrieveAll()
        {
            return db.Locations
                .ToList<Location>();
        }

        public Location Update(Location l)
        {
            db.Locations.Update(l);
            int affected = db.SaveChanges();
            if (affected <= 1)
            {
                return null;
            }
            return l;
        }
    }
}
