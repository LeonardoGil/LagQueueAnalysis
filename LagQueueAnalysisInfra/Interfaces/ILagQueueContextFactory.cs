using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;

namespace LagQueueAnalysisInfra.Interfaces
{
    public interface ILagQueueContextFactory
    {
        LagQueueContext Create(AnalysisEnvironment environment, string connectionString);
    }
}
