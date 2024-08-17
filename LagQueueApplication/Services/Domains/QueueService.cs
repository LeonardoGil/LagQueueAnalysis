using LagQueueApplication.Interfaces;
using LagQueueDomain.Comparers;
using LagQueueDomain.Entities;

namespace LagQueueApplication.Services.Domains
{
    public class QueueService : IQueueService
    {
        private readonly IBaseRepository _repository;

        public QueueService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public void Register(List<Queue> queues)
        {
            var queuesUpdates = _repository.Get<Queue>()
                                           .Where(queue => queues.Contains(queue, QueueEqualityComparer.Create()))
                                           .ToList();

            var queuesAdds = queues.Except(queuesUpdates, QueueEqualityComparer.Create()).ToList();

            // No momento não há necessidade de atualizar as Queues
            // verificar a necessidade no Futuro...

            _repository.AddRange(queuesAdds);
            _repository.SaveChanges();
        }
    }
}
