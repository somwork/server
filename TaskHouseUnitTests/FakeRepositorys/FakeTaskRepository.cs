using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TaskHouseUnitTests.FakeRepositorys
{
    public class FakeTaskRepository : FakeRepository<Task>, ITaskRepository
    {

        public FakeTaskRepository()
            : base(new List<TaskHouseApi.Model.Task>()
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
            }
            )
        { }
    }
}

