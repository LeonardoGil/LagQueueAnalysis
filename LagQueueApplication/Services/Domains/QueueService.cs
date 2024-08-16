using LagQueueApplication.Interfaces;
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
            _repository.AddRange(queues);
            _repository.SaveChanges();
        }
    }
}
