using LagQueueApplication.Services;

namespace LagQueueApplication.Interfaces
{
    public interface IExecuteProcessingEventService
    {
        Task<Guid> On<Command, TBackgroundService>(Command obj, string serviceName = nameof(ExecuteProcessingEventService)) where TBackgroundService : IProcessingEvent<Command>;
    }
}
