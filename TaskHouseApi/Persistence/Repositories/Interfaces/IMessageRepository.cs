using TaskHouseApi.Model;
using System.Collections.Generic;

namespace TaskHouseApi.Persistence.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        IEnumerable<Message> RetrieveAllMessagesForSpecificTaskId(int Id);
    }
}
