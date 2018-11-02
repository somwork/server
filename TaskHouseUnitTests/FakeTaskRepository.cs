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
    public class FakeTaskRepository : ITaskRepository
    { 
        private static List<TaskHouseApi.Model.Task> taskCache;

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

         public async Task<TaskHouseApi.Model.Task> Create(TaskHouseApi.Model.Task t)
        {
            taskCache.Add(t);
            return t;
        }

        public async Task<IEnumerable<TaskHouseApi.Model.Task>> RetrieveAll()
        {
            return taskCache;
        }

        public async Task<TaskHouseApi.Model.Task> Retrieve(int id)
        {
            return taskCache.Where(t=> t.Id == id).SingleOrDefault();
        }


        public async Task<TaskHouseApi.Model.Task> Update(int Id, TaskHouseApi.Model.Task t)
        {
            TaskHouseApi.Model.Task old = taskCache.Where(task => task.Id == Id).SingleOrDefault();
            int index = taskCache.IndexOf(old);

            taskCache[index] = t;
            return t;

        }

        public async Task<bool> Delete(int Id)
        {
            TaskHouseApi.Model.Task task = taskCache.Where(t => t.Id == Id).SingleOrDefault();

            if (task == null) return false;

            taskCache.Remove(task);
            return true;

        }
    }
}