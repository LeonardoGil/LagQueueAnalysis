using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Models;
using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;

namespace LagEnvironmentApplication.Services.Domains
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IBaseRepository<LagEnvironmentContext> _baseRepository;
        private readonly ILagQueueContextFactory _lagQueueContextFactory;

        public EnvironmentService(IBaseRepository<LagEnvironmentContext> baseRepository, 
                                  ILagQueueContextFactory lagQueueContextFactory)
        {
            _baseRepository = baseRepository;
            _lagQueueContextFactory = lagQueueContextFactory;
        }

        public async Task<AnalysisEnvironment> ToGenerate(GenerateEnvironmentModel generateEnvironment)
        {
            var environment = _baseRepository.Get<AnalysisEnvironment>(x => x.Url == generateEnvironment.Url.Authority).FirstOrDefault();

            if (environment is null)
            {
                environment = new AnalysisEnvironment
                {
                    Description = generateEnvironment.Description,
                    Url = generateEnvironment.Url.Authority,
                    Database = $"LagQueueAnalysis_{GetDomainName(generateEnvironment.Url.Host)}"
                };

                _baseRepository.Add(environment);
                _baseRepository.SaveChanges();

                var context = _lagQueueContextFactory.Create(environment);
                context.Database.EnsureCreated();
            }

            return environment;
        }

        private string GetDomainName(string host)
        {
            var segments = host.Split('.');

            if (segments.Length >= 2)
                return segments[1];

            return host;
        }
    }
}
