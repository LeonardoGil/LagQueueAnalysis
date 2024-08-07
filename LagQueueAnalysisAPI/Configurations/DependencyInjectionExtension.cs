using LagQueueApplication.EFContexts;
using Microsoft.EntityFrameworkCore;

namespace LagQueueAnalysisAPI.Configurations
{
    public static class DependencyInjectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LagQueueAnalysisDB");
            
            services.AddDbContext<LagQueueContext>(options => options.UseSqlServer(connectionString, o => o.MigrationsAssembly("LagQueueApplication")));
        }
    }
}
