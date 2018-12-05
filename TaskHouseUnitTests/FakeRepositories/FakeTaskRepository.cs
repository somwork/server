using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeTaskRepository : FakeRepository<Task>, ITaskRepository
    {

        public FakeTaskRepository()
            : base(new List<TaskHouseApi.Model.Task>()
            {
                new TaskHouseApi.Model.Task()
                {
                    Id = 1,
                    Description = "Task1",
                    EmployerId = 1
                },
                new TaskHouseApi.Model.Task()
                {
                    Id = 2,
                    Description = "Task2",
                    EmployerId = 1
                },
                new TaskHouseApi.Model.Task()
                {
                    Id = 3,
                    Description = "Task3",
                    EmployerId = 1
                },
                new TaskHouseApi.Model.Task()
                {
                    Id = 4,
                    Description = "Task4",
                    EmployerId = 2
                },
            }
            )
        { }

        public IEnumerable<Task> GetTasksForEmployer(int Id)
        {
            return list.Where(e => e.EmployerId == Id).ToList();
        }

        public IEnumerable<Task> GetAcceptedTasksForEmployer(int Id)
        {
            return list
                .Where(task => task.EmployerId == Id && task.Estimates.Any(w => w.Accepted && task.Id == w.TaskId))
                .ToList();
        }

        public IEnumerable<Task> GetAcceptedTasksForWorker(int Id)
        {
            return list
                .Where(task => task.Estimates.Any(w => w.WorkerId == Id && w.Accepted && task.Id == w.TaskId))
                .ToList();
        }

        public IEnumerable<Task> GetEstimatedTasksForWorker(int Id)
        {
            return list
                .Where(task => task.Estimates.Any(w => w.WorkerId == Id && w.Accepted == false && task.Id == w.TaskId))
                .ToList();
        }

        //get all available tasks for workers.
        public IEnumerable<Task> GetAvailableTasksForWorker()
        {
            double temp = 0;

            return list
                .Where(task => task.AverageEstimate == temp || task.Estimates.Any(e => e.Accepted == false))
                .ToList();
        }
    }
}

