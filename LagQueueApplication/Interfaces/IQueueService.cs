using LagQueueDomain.Entities;

namespace LagQueueApplication.Interfaces
{
    public interface IQueueService
    {
        void Register(List<Queue> queues);
    }
}
