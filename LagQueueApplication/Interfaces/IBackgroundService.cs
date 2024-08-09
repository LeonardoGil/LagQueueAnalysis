namespace LagQueueApplication.Interfaces
{
    public interface IBackgroundService
    {
        Task BackgroundRun(Command command);
    }
}
