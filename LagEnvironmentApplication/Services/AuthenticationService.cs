using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Models;
using LagRabbitMQ.Interfaces;
using LagRabbitMQ.Settings;

namespace LagEnvironmentApplication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IOverviewRabbitService _overviewService;
        private readonly IEnvironmentService _environmentService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IOverviewRabbitService overviewService,
                                     IEnvironmentService environmentService,
                                     ITokenService tokenService)
        {
            _overviewService = overviewService;
            _environmentService = environmentService;
            _tokenService = tokenService;
        }

        public async Task<string> Authenticate(AuthenticateModel authenticate)
        {
            if (await ValidateConnectionWithRabbitMQ(authenticate))
                return "Conexão com RabbitMQ Inválida";

            var generateEnvironment = new GenerateEnvironmentModel
            {
                Url = new Uri(authenticate.Url),
                Description = string.Empty
            };

            var environment = await _environmentService.ToGenerate(generateEnvironment);

            var token = _tokenService.Register(environment);

            return token.Key;
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
