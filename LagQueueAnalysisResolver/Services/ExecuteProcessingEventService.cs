using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Stores;
using LagEnvironmentDomain.Entities;
using LagQueueAnalysisResolver.Configurations;
using LagQueueApplication.Interfaces;
using LagQueueDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LagQueueAnalysisResolver.Services
{
    public class ExecuteProcessingEventService : IExecuteProcessingEventService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IProcessingEventService _processingEventService;
        private readonly IHttpContextAccessor _httpContextAcessor;
        private readonly IConfiguration _configuration;
        private readonly ITokenStore _tokenStore;

        public ExecuteProcessingEventService(IServiceScopeFactory scopeFactory, IProcessingEventService processingEventService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITokenStore tokenStore)
        {
            _scopeFactory = scopeFactory;
            _processingEventService = processingEventService;
            _httpContextAcessor = httpContextAccessor;
            _configuration = configuration;
            _tokenStore = tokenStore;
        }

        public async Task<Guid> On<Event, ProcessingEventService>(Event obj, string serviceName = nameof(ExecuteProcessingEventService))
            where ProcessingEventService : IProcessingEvent<Event>
        {
            ProcessingEvent processingEvent = _processingEventService.Register(serviceName);

            var environment = GetEnvironment();

            var environmentConnectionString = _configuration.GetConnectionString("LagEnvironmentDB");

            var queueConnectionString = _configuration.GetConnectionString("LagQueueAnalysisDB");

            _ = Task.Run(async () =>
            {
                var servicesProvider = GetServicesProvider<ProcessingEventService>(environment, environmentConnectionString, queueConnectionString);

                IProcessingEventService processingEventService = default;

                try
                {
                    processingEventService = servicesProvider.GetRequiredService<IProcessingEventService>();

                    var service = servicesProvider.GetRequiredService<ProcessingEventService>();

                    await service.Run(obj);

                    processingEventService.ProcessSuccess(processingEvent);
                }
                catch (Exception exception)
                {
                    processingEventService.ProcessFail(processingEvent, exception);
                }
            });

            return processingEvent.Id;
        }

        private IServiceProvider GetServicesProvider<ProcessingEventService>(AnalysisEnvironment environment, string environmentConnectionString = "", string queueConnectionString = "")
        {
            var services = new ServiceCollection();

            services.AddServices();

            services.AddRabbitMQServices();

            services.AddLagEnvironmentContext(environmentConnectionString);

            services.AddLagQueueContext(queueConnectionString);

            services.AddMappers();

            services.AddTokenAcessor(environment);

            return services.BuildServiceProvider();
        }

        private AnalysisEnvironment GetEnvironment()
        {
            var authorization = _httpContextAcessor.HttpContext?.Request.Headers["Authorization"];

            var tokenKey = Guid.Parse(authorization.ToString() ?? throw new Exception("Token Authorization não informado"));

            return _tokenStore.GetEnvironment(tokenKey) ?? throw new Exception("Token Inválido");
        }
    }
}
