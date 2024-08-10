using LagQueueApplication.EFContexts;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings;
using LagQueueApplication.Repository;
using LagQueueApplication.Services;
using LagQueueApplication.Services.Domains;
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
            services.AddTransient<IProcessingEventService, ProcessingEventService>();
            services.AddTransient<IExecuteProcessingEventService, ExecuteProcessingEventService>();

            // Events
            services.AddTransient<IQueueRegisterProcessingEvent, QueueRegisterProcessingEvent>();

        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LagQueueAnalysisDB");

            services.AddDbContext<LagQueueContext>(options => options.UseSqlServer(connectionString, o => o.MigrationsAssembly("LagQueueApplication")));
        }
    }
}
