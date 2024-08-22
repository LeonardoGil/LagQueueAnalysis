using LagQueueApplication.Processings.Events;

namespace LagQueueApplication.Interfaces
{
    public interface IRegisterQueueMessagesProcessingEvent : IProcessingEvent<RegisterQueueMessagesEvent>
    {
    }
}
