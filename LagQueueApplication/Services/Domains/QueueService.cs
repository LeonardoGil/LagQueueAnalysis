using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueApplication.Interfaces;
using LagQueueDomain.Comparers;
using LagQueueDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LagQueueApplication.Services.Domains
{
    public class QueueService(IBaseRepository<LagQueueContext> repository) : IQueueService
    {
        private readonly IBaseRepository<LagQueueContext> _repository = repository;

        public void Register(List<Queue> queues)
        {
            var queuesUpdates = _repository.Get<Queue>().AsNoTracking().ToList();
            
            var queuesAdds = queues.Except(queuesUpdates, QueueEqualityComparer.Create()).ToList();

            // No momento não há necessidade de atualizar as Queues
            // verificar a necessidade no Futuro...

            if (queuesAdds.Count == 0)
                return;

            _repository.AddRange(queuesAdds);
            _repository.SaveChanges();
        }
    }
}
