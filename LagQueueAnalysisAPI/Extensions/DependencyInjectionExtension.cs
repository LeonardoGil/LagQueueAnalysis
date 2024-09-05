using LagQueueAnalysisResolver.Configurations;

namespace LagQueueAnalysisAPI.Configurations
{
    public static class DependencyInjectionExtension
    {
        public static void Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddServices();
            
            services.AddRabbitMQServices();
            
            services.AddLagEnvironmentContext(configuration);
            
            services.AddLagQueueContext(configuration);

            services.AddMappers();
        }
    }
}
