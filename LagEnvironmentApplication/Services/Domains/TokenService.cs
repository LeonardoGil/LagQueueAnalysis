using LagEnvironmentApplication.Interfaces;
using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;

namespace LagEnvironmentApplication.Services.Domains
{
    public class TokenService : ITokenService
    {
        private readonly IBaseRepository<LagEnvironmentContext> _baseRepository;

        public TokenService(IBaseRepository<LagEnvironmentContext> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Token Register(AnalysisEnvironment environment)
        {
            var token = new Token
            {
                Expiration = DateTime.UtcNow.AddDays(90),
                Key = Guid.NewGuid().ToString(),
                AnalysisEnvironmentId = environment.Id
            };

            _baseRepository.Add(token);
            _baseRepository.SaveChanges();

            return token;
        }
    }
}
