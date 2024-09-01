using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Services;
using LagEnvironmentApplication.Services.Domains;
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
            services.AddTransient<IBaseRepository<LagQueueContext>, BaseRepository<LagQueueContext>>();
            services.AddTransient<IBaseRepository<LagEnvironmentContext>, BaseRepository<LagEnvironmentContext>>();
            services.AddTransient<IQueueRepository, QueueRepository>();

            // Services
            services.AddTransient<IExecuteProcessingEventService, ExecuteProcessingEventService>();

            // Services Domain Queue
            services.AddTransient<IProcessingEventService, ProcessingEventService>();
            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<IMessageService, MessageService>();

            // Services Domain Environment
            services.AddTransient<IEnvironmentService, EnvironmentService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            // Services Rabbit
            services.AddTransient<IQueueRabbitServices, QueueRabbitService>();
            services.AddTransient<IOverviewRabbitService, OverviewRabbitService>();

            // Events
            services.AddTransient<IRegisterQueueProcessingEvent, RegisterQueueProcessingEvent>();
            services.AddTransient<IRegisterQueueMessagesProcessingEvent, RegisterQueueMessagesProcessingEvent>();

            // Factories
            services.AddTransient<ILagQueueContextFactory, LagQueueContextFactory>();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var environmentConnectionString = configuration.GetConnectionString("LagEnvironmentDB");
            services.AddDbContext<LagEnvironmentContext>(options => options.UseSqlServer(environmentConnectionString, o => o.MigrationsAssembly(nameof(LagQueueAnalysisInfra))));

            services.AddHttpContextAccessor();
            services.AddScoped<LagQueueContext>(provider =>
            {
                var httpContextAcessor = provider.GetRequiredService<IHttpContextAccessor>();
                var token = httpContextAcessor.HttpContext?.Request.Headers.Authorization.ToString() ?? throw new Exception("Não foi possivel buscar o token");

                var factory = provider.GetRequiredService<ILagQueueContextFactory>();
                return factory.Create(token);
            });
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationModule));
        }
    }
}
