using LagQueueApplication.Models;

namespace LagQueueApplication.Interfaces
{
    public interface IQueueQuery
    {
        public List<QueueQueryModel> List();
    }
}
