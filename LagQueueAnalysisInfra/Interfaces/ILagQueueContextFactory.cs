using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;

namespace LagQueueAnalysisInfra.Interfaces
{
    public interface ILagQueueContextFactory
    {
        LagQueueContext Create(string token);
        LagQueueContext Create(AnalysisEnvironment environment);

    }
}
