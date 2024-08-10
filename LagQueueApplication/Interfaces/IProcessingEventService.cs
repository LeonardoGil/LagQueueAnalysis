using LagQueueDomain.Entities;

namespace LagQueueApplication.Interfaces
{
    public interface IProcessingEventService
    {
        ProcessingEvent Register(string name);

        void ProcessFail(ProcessingEvent processingEvent, Exception ex);

        void ProcessSuccess(ProcessingEvent processingEvent);
    }
}
