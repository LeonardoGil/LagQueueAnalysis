using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LagQueueAnalysisInfra.Factories
{
    public class LagQueueContextFactory : ILagQueueContextFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IBaseRepository<LagEnvironmentContext> _baseRepository;

        public LagQueueContextFactory(IConfiguration configuration,
                                      IBaseRepository<LagEnvironmentContext> baseRepository)
        {
            _configuration = configuration;
            _baseRepository = baseRepository;
        }

        public LagQueueContext Create(string token)
        {
            var environment = _baseRepository.Get<Token>(x => x.Key == token)
                                             .Include(x => x.AnalysisEnvironment)
                                             .FirstOrDefault()?.AnalysisEnvironment ?? throw new UnauthorizedAccessException("Token inválido");

            return Create(environment);
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
