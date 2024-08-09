using LagQueueApplication.Processings.Events;

namespace LagQueueApplication.Interfaces
{
    public interface IQueueRegisterProcessingEvent : IProcessingEvent<QueueRegisterEvent>
    {
    }
}
