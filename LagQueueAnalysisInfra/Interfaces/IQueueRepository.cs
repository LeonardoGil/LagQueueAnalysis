using LagQueueDomain.Entities;

namespace LagQueueAnalysisInfra.Interfaces
{
    public interface IQueueRepository
    {
        Queue? GetByName(string name);
    }
}
