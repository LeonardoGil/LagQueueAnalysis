using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Models;
using LagEnvironmentDomain.Entities;
using LagRabbitMqManagerToolkit.Domains;
using LagRabbitMqManagerToolkit.Services.Interfaces;

namespace LagEnvironmentApplication.Services
{
    public class AuthenticationService(IOverviewService _overviewService,
                                       IEnvironmentService _environmentService,
                                       ITokenStore _tokenStore) : IAuthenticationService
    {
        public async Task<Guid> Authenticate(AuthenticateModel authenticate)
        {
            var rabbitMQSetting = await ValidateConnectionWithRabbitMQ(authenticate);

            var generateEnvironment = new GenerateEnvironmentModel
            {
                Url = new Uri(authenticate.Url),
                Description = string.Empty
            };

            var environment = await _environmentService.ToGenerate(generateEnvironment);

            environment.RabbitSettings = rabbitMQSetting;

            var token = await ValidateToken(environment);

            return token.Id;
        }

        private async Task<Token> ValidateToken(AnalysisEnvironment environment)
        {
            var token = _tokenStore.GetValidTokenByEnvironmentId(environment.Id);

            if (token is null)
            {
                token = Token.Create(environment);

                _tokenStore.SetToken(token.Id, token);
            }

            return await Task.FromResult(token);
        }

        private async Task<RabbitSettings> ValidateConnectionWithRabbitMQ(AuthenticateModel authenticate)
        {
            var rabbitMQSetting = new RabbitSettings
            {
                Url = authenticate.Url,
                Username = authenticate.User,
                Password = authenticate.Password
            };

            try
            {
                await _overviewService.GetAsync(rabbitMQSetting);

                return rabbitMQSetting;
            }
            catch (Exception ex)
            {
                throw new Exception("Conexão com RabbitMQ Inválida");
            }
        }
    }
}
