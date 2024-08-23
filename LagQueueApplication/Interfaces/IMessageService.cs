using LagQueueDomain.Entities;

namespace LagQueueApplication.Interfaces
{
    public interface IMessageService
    {
        void Register(List<Message> queues);
    }
}
