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
            if (await ValidateConnectionWithRabbitMQ(authenticate))
                throw new Exception("Conexão com RabbitMQ Inválida");

            var generateEnvironment = new GenerateEnvironmentModel
            {
                Url = new Uri(authenticate.Url),
                Description = string.Empty
            };

            var environment = await _environmentService.ToGenerate(generateEnvironment);

            var token = _tokenStore.GetValidTokenByEnvironmentId(environment.Id);

            if (token is null)
            {
                token = Token.Create(environment);
                
                _tokenStore.SetToken(token.Id, token);
            }

            return token.Id;
        }

        private async Task<bool> ValidateConnectionWithRabbitMQ(AuthenticateModel authenticate)
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

                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }
    }
}
