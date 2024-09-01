using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LagQueueAnalysisInfra.Factories
{
    public class LagQueueContextFactory : ILagQueueContextFactory
    {
        private readonly IConfiguration _configuration;

        public LagQueueContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LagQueueContext Create(AnalysisEnvironment environment)
        {
            var connectionString = string.Format(_configuration.GetConnectionString("LagQueueAnalysisDB") ?? throw new Exception("ConnectionString não encontrada pelo AppSettings"),
                                                 environment.Database);

            var options = new DbContextOptionsBuilder<LagQueueContext>().UseSqlServer(connectionString).Options;

            return new LagQueueContext(options);
        }
    }
}
