using System;
using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeMessageRepository : FakeRepository<Message>, IMessageRepository
    {
        public FakeMessageRepository()
            : base(new List<Message>()
                {
                    new Message()
                    {
                        Id = 1,
                        Text = "text",
                        SendAt = new DateTime(2018, 3, 3),
                        UserId = 1,
                        TaskId = 1
                    },
                    new Message()
                    {
                        Id = 2,
                        Text = "text",
                        SendAt = new DateTime(2018, 3, 3),
                        UserId = 1,
                        TaskId = 1
                    },
                    new Message()
                    {
                        Id = 3,
                        Text = "text",
                        SendAt = new DateTime(2018, 3, 3),
                        UserId = 1,
                        TaskId = 1
                    }
                }
            )
        { }
    }
}
