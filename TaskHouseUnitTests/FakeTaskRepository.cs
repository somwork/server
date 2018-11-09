using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TaskHouseUnitTests
{
    public class FakeTaskRepository : ITaskRepository
    {
        private static List<Task> taskCache;

        public FakeTaskRepository()
        {
            taskCache = new List<TaskHouseApi.Model.Task>()
            {
                new TaskHouseApi.Model.Task()
                {
                    Id = 1,
                    Description = "Task1"
                },
                new TaskHouseApi.Model.Task()
                {
                    Id = 2,
                    Description = "Task2"
                },
                new TaskHouseApi.Model.Task()
                {
                    Id = 3,
                    Description = "Task3"
                },
                new TaskHouseApi.Model.Task()
                {
                    Id = 4,
                    Description = "Task4"
                },
            };
        }

        public Task Create(Task t)
        {
            taskCache.Add(t);
            return t;
        }

        public IEnumerable<Task> RetrieveAll()
        {
            return taskCache;
        }

        public Task Retrieve(int id)
        {
            return taskCache.Where(t => t.Id == id).SingleOrDefault();
        }

        public Task Update(Task t)
        {
            Task old = Retrieve(t.Id);
            int index = taskCache.IndexOf(old);

            taskCache[index] = t;
            return t;
        }

        public bool Delete(int Id)
        {
            Task temp = taskCache.Find(t => t.Id == Id);
            taskCache.Remove(temp);
            temp = taskCache.Find(t => t.Id == Id);

            if (temp != null)
            {
                return false;
            }
            return true;
        }
    }
}
