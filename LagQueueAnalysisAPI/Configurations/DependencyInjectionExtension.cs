using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Services;
using LagEnvironmentApplication.Services.Domains;
using LagEnvironmentApplication.Stores;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Factories;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueAnalysisInfra.Repositories;
using LagQueueApplication;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings;
using LagQueueApplication.Services;
using LagQueueApplication.Services.Domains;
using LagRabbitMQ.Interfaces;
using LagRabbitMQ.Services;
using Microsoft.EntityFrameworkCore;

namespace LagQueueAnalysisAPI.Configurations
{
    public static class DependencyInjectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IBaseRepository<LagQueueContext>, BaseRepository<LagQueueContext>>();
            services.AddScoped<IBaseRepository<LagEnvironmentContext>, BaseRepository<LagEnvironmentContext>>();
            services.AddScoped<IQueueRepository, QueueRepository>();

            // Services
            services.AddScoped<IExecuteProcessingEventService, ExecuteProcessingEventService>();

            // Services Domain Queue
            services.AddScoped<IProcessingEventService, ProcessingEventService>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddScoped<IMessageService, MessageService>();

            // Services Domain Environment
            services.AddScoped<IEnvironmentService, EnvironmentService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Services Rabbit
            services.AddScoped<IQueueRabbitService, QueueRabbitService>();
            services.AddScoped<IOverviewRabbitService, OverviewRabbitService>();

            // Events
            services.AddScoped<IRegisterQueueProcessingEvent, RegisterQueueProcessingEvent>();
            services.AddScoped<IRegisterQueueMessagesProcessingEvent, RegisterQueueMessagesProcessingEvent>();

            // Factories
            services.AddScoped<ILagQueueContextFactory, LagQueueContextFactory>();

            // Store
            services.AddSingleton<ITokenStore, TokenStore>(); // Gerencia os tokens de Autenticação

            // Outros
            services.AddHttpContextAccessor();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var environmentConnectionString = configuration.GetConnectionString("LagEnvironmentDB");
            services.AddDbContext<LagEnvironmentContext>(options => options.UseSqlServer(environmentConnectionString, o => o.MigrationsAssembly(nameof(LagQueueAnalysisInfra))));

            services.AddScoped<LagQueueContext>(provider =>
            {
                var httpContextAcessor = provider.GetRequiredService<IHttpContextAccessor>();
                var contextFactory = provider.GetRequiredService<ILagQueueContextFactory>();
                var tokenStore = provider.GetRequiredService<ITokenStore>();


                var authorization = httpContextAcessor.HttpContext?.Request.Headers.Authorization;
                
                var tokenKey = Guid.Parse(authorization.ToString() ?? throw new Exception("Token Authorization não informado"));

                var environment = tokenStore.GetEnvironment(tokenKey);

                return contextFactory.Create(environment);
            });
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationModule));
        }
    }
}
