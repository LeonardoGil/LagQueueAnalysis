using LagQueueDomain.Entities;

namespace LagQueueApplication.Interfaces
{
    public interface IQueueRepository
    {
        Queue? GetByName(string name);
    }
}
