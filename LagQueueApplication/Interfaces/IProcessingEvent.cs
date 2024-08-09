namespace LagQueueApplication.Interfaces
{
    public interface IProcessingEvent<Command>
    {
        Task Run(Command command);
    }
}
