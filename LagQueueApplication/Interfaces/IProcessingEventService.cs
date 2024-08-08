namespace LagQueueApplication.Interfaces
{
    public interface IProcessingEventService
    {
        Guid Register(string name);
    }
}
