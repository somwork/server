using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        //reference to database context
        private PostgresContext db;

        public SkillRepository(PostgresContext db)
        {
            this.db = db;
        }

        public Skill Create(Skill s)
        {
            //add skill to database using ef core
            db.Skills.Add(s);

            int affected = db.SaveChanges();

            //skill not created
            if (affected != 1)
            {
                return null;
            }
            return s;
        }

        public IEnumerable<Skill> RetrieveAll()
        {
            return db.Skills
                .ToList<Skill>();
        }

        public Skill Retrieve(int Id)
        {
            return db.Skills
                .Where(skill => skill.Id == Id)
                .SingleOrDefault();
        }

        public Skill Update(Skill s)
        {
            db.Skills.Update(s);
            int affected = db.SaveChanges();
            if (affected <= 1)
            {
                return null;
            }
            return s;
        }

        public bool Delete(int Id)
        {
            Skill s = db.Skills.Find(Id);
            db.Skills.Remove(s);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return false;
            }
            return true;
        }
    }
}
