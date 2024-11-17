using LagQueueApplication.Models;

namespace LagQueueApplication.Interfaces
{
    public interface IProcessingEventQuery
    {
        ProcessingEventQueryModel GetById(Guid id);

        IList<ProcessingEventQueryModel> GetLastEvents(int take = 10);
    }
}
