using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LagQueueAnalysisInfra.Factories
{
    public class LagQueueContextFactory : ILagQueueContextFactory
    {
        public LagQueueContextFactory()
        {
        }

        public LagQueueContext Create(AnalysisEnvironment environment, string connectionString)
        {
            var options = new DbContextOptionsBuilder<LagQueueContext>().UseSqlServer(connectionString).Options;

            return new LagQueueContext(options);
        }
    }
}
