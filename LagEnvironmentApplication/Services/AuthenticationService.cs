using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Models;
using LagEnvironmentDomain.Entities;
using LagRabbitMQ.Interfaces;
using LagRabbitMQ.Settings;
using System;

namespace LagEnvironmentApplication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IOverviewRabbitService _overviewService;
        private readonly IEnvironmentService _environmentService;
        private readonly ITokenStore _tokenStore;

        public AuthenticationService(IOverviewRabbitService overviewService,
                                     IEnvironmentService environmentService,
                                     ITokenStore tokenStore)
        {
            _overviewService = overviewService;
            _environmentService = environmentService;
            _tokenStore = tokenStore;
        }

        public async Task<Guid> Authenticate(AuthenticateModel authenticate)
        {
            var rabbitMQSetting = await ValidateConnectionWithRabbitMQ(authenticate);

            var generateEnvironment = new GenerateEnvironmentModel
            {
                Url = new Uri(authenticate.Url),
                Description = string.Empty
            };

            var environment = await _environmentService.ToGenerate(generateEnvironment);

            environment.RabbitMQSetting = rabbitMQSetting;

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

        private async Task<RabbitMQSetting> ValidateConnectionWithRabbitMQ(AuthenticateModel authenticate)
        {
            var rabbitMQSetting = new RabbitMQSetting
            {
                Url = authenticate.Url,
                Username = authenticate.User,
                Password = authenticate.Password
            };

            try
            {
                await _overviewService.OverviewRequest(rabbitMQSetting);

                return rabbitMQSetting;
            }
            catch (Exception)
            {
                throw new Exception("Conexão com RabbitMQ Inválida");
            }
        }
    }
}
