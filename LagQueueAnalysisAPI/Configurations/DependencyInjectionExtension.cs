using LagQueueApplication;
using LagQueueApplication.EFContexts;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings;
using LagQueueApplication.Repository;
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

            // Services
            services.AddTransient<IExecuteProcessingEventService, ExecuteProcessingEventService>();

            // Services Domain
            services.AddTransient<IProcessingEventService, ProcessingEventService>();
            services.AddTransient<IQueueService, QueueService>();
            
            // Services Rabbit
            services.AddTransient<IQueueRabbitServices, QueueRabbitServices>();

            // Events
            services.AddTransient<IRegisterQueueProcessingEvent, RegisterQueueProcessingEvent>();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LagQueueAnalysisDB");

            services.AddDbContext<LagQueueContext>(options => options.UseSqlServer(connectionString, o => o.MigrationsAssembly("LagQueueApplication")));
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationModule));
        }
    }
}
