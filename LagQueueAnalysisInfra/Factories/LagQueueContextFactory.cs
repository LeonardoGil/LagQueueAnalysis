using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LagQueueAnalysisInfra.Factories
{
    public class LagQueueContextFactory : ILagQueueContextFactory
    {
        public LagQueueContextFactory()
        {
        }

        public LagQueueContext Create(AnalysisEnvironment environment, string connectionString)
        {
            var options = new DbContextOptionsBuilder<LagQueueContext>().UseSqlServer(connectionString, x => x.MigrationsAssembly(nameof(LagQueueAnalysisInfra))).Options;

            var context = new LagQueueContext(options);
            
            context.Database.Migrate();

            return context;
        }
    }
}
