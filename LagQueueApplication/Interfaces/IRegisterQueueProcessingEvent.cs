using LagQueueApplication.Processings.Events;

namespace LagQueueApplication.Interfaces
{
    public interface IRegisterQueueProcessingEvent : IProcessingEvent<RegisterQueueEvent>
    {
    }
}
