using LagQueueApplication.EFContexts;
using LagQueueApplication.Interfaces;
using LagQueueDomain.Entities;

namespace LagQueueApplication.Repository
{
    public class QueueRepository : BaseRepository, IQueueRepository
    {
        public QueueRepository(LagQueueContext dbContext) : base(dbContext)
        {
        }

        public Queue? GetByName(string name)
        {
            return Get<Queue>(queue => queue.Name == name).FirstOrDefault();
        }
    }
}
