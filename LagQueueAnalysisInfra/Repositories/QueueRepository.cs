using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueDomain.Entities;

namespace LagQueueAnalysisInfra.Repositories
{
    public class QueueRepository : BaseRepository<LagQueueContext>, IQueueRepository
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
