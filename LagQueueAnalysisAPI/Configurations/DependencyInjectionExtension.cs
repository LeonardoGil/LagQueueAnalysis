using LagQueueAnalysisInfra.EFContexts;
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
            // Repository
            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<IQueueRepository, QueueRepository>();

            // Services
            services.AddTransient<IExecuteProcessingEventService, ExecuteProcessingEventService>();

            // Services Domain
            services.AddTransient<IProcessingEventService, ProcessingEventService>();
            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<IMessageService, MessageService>();
            
            // Services Rabbit
            services.AddTransient<IQueueRabbitServices, QueueRabbitServices>();

            // Events
            services.AddTransient<IRegisterQueueProcessingEvent, RegisterQueueProcessingEvent>();
            services.AddTransient<IRegisterQueueMessagesProcessingEvent, RegisterQueueMessagesProcessingEvent>();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var queueConnectionString = configuration.GetConnectionString("LagQueueAnalysisDB");
            services.AddDbContext<LagQueueContext>(options => options.UseSqlServer(queueConnectionString, o => o.MigrationsAssembly(nameof(LagQueueAnalysisInfra))));

            var environmentConnectionString = configuration.GetConnectionString("LagEnvironmentDB");
            services.AddDbContext<LagEnvironmentContext>(options => options.UseSqlServer(environmentConnectionString, o => o.MigrationsAssembly(nameof(LagQueueAnalysisInfra))));
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationModule));
        }
    }
}
