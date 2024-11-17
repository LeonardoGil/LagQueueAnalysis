using LagEnvironmentApplication.Interfaces;
using LagEnvironmentApplication.Services;
using LagEnvironmentApplication.Services.Domains;
using LagEnvironmentApplication.Stores;
using LagEnvironmentDomain.Entities;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Factories;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueAnalysisInfra.Repositories;
using LagQueueAnalysisResolver.Services;
using LagQueueApplication;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings;
using LagQueueApplication.Queries;
using LagQueueApplication.Services.Domains;
using LagRabbitMQ.Interfaces;
using LagRabbitMQ.Services;
using LagRabbitMQ.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LagQueueAnalysisResolver.Configurations
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

            // Events
            services.AddScoped<IRegisterQueueProcessingEvent, RegisterQueueProcessingEvent>();
            services.AddScoped<IRegisterQueueMessagesProcessingEvent, RegisterQueueMessagesProcessingEvent>();

            // Factories
            services.AddScoped<ILagQueueContextFactory, LagQueueContextFactory>();

            // Store
            services.AddSingleton<ITokenStore, TokenStore>();

            // Queries
            services.AddScoped<IQueueQuery, QueueQuery>();
            services.AddScoped<IMessageQuery, MessageQuery>();
            services.AddScoped<IProcessingEventQuery, ProcessingEventQuery>();
        }

        public static void AddRabbitMQServices(this IServiceCollection services)
        {
            // Services Rabbit
            services.AddScoped<IQueueRabbitService, QueueRabbitService>();
            services.AddScoped<IOverviewRabbitService, OverviewRabbitService>();

            services.AddScoped<RabbitMQSetting>(provider =>
            {
                IHttpContextAccessor httpContextAcessor = default;

                try
                {
                    httpContextAcessor = provider.GetRequiredService<IHttpContextAccessor>();
                }
                catch (Exception)
                {
                    // Em MultiThread não é possivel injetar o IHttpContextAcessor
                    // Ao Solicitar o serviço, a aplicação lança uma exceção
                    // Esse processo não pode interromper o fluxo
                }

                if (httpContextAcessor is not null)
                {
                    var tokenStore = provider.GetRequiredService<ITokenStore>();

                    var authorization = httpContextAcessor.HttpContext?.Request.Headers["Authorization"];

                    var tokenKey = Guid.Parse(authorization.ToString() ?? throw new Exception("Token Authorization não informado"));

                    return tokenStore.GetEnvironment(tokenKey)?.RabbitMQSetting ?? throw new Exception("RabbitMQ Setting não encontrado");
                }

                var tokenAcessor = provider.GetRequiredService<TokenAcessor>();

                if (tokenAcessor is not null)
                {
                    return tokenAcessor.AnalysisEnvironment.RabbitMQSetting;
                }

                return null;
            });
        }

        public static void AddTokenAcessor(this IServiceCollection services, AnalysisEnvironment environment)
        {
            services.AddScoped<TokenAcessor>(provider => new() { AnalysisEnvironment = environment });
        }

        public static void AddLagEnvironmentContext(this IServiceCollection services, IConfiguration configuration)
        {
            var environmentConnectionString = configuration.GetConnectionString("LagEnvironmentDB");
            services.AddDbContext<LagEnvironmentContext>(options => options.UseSqlServer(environmentConnectionString, o => o.MigrationsAssembly(nameof(LagQueueAnalysisInfra))));
        }

        public static void AddLagEnvironmentContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LagEnvironmentContext>(options => options.UseSqlServer(connectionString, o => o.MigrationsAssembly(nameof(LagQueueAnalysisInfra))));
        }

        public static void AddLagQueueContext(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<LagQueueContext>(provider =>
            {
                var contextFactory = provider.GetRequiredService<ILagQueueContextFactory>();

                IHttpContextAccessor httpContextAcessor = default;

                try
                {
                    httpContextAcessor = provider.GetRequiredService<IHttpContextAccessor>();
                }
                catch (Exception)
                {
                    // Em MultiThread não é possivel injetar o IHttpContextAcessor
                    // Ao Solicitar o serviço, a aplicação lança uma exceção
                    // Esse processo não pode interromper o fluxo
                }

                if (httpContextAcessor is not null)
                {
                    var tokenStore = provider.GetRequiredService<ITokenStore>();

                    var authorization = httpContextAcessor.HttpContext?.Request.Headers["Authorization"];

                    var tokenKey = Guid.Parse(authorization.ToString() ?? throw new Exception("Token Authorization não informado"));

                    var environment = tokenStore.GetEnvironment(tokenKey);

                    return contextFactory.Create(environment, string.Format(connectionString, environment.Database));
                }

                var tokenAcessor = provider.GetRequiredService<TokenAcessor>();

                if (tokenAcessor is not null)
                {
                    return contextFactory.Create(tokenAcessor.AnalysisEnvironment, string.Format(connectionString, tokenAcessor.AnalysisEnvironment.Database));
                }

                return null;
            });
        }

        public static void AddLagQueueContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<LagQueueContext>(provider =>
            {
                var contextFactory = provider.GetRequiredService<ILagQueueContextFactory>();
                
                IHttpContextAccessor httpContextAcessor = default;

                try
                {
                    httpContextAcessor = provider.GetRequiredService<IHttpContextAccessor>();
                }
                catch (Exception)
                {
                    // Em MultiThread não é possivel injetar o IHttpContextAcessor
                    // Ao Solicitar o serviço, a aplicação lança uma exceção
                    // Esse processo não pode interromper o fluxo
                }

                var connectionString = configuration.GetConnectionString("LagQueueAnalysisDB");

                if (httpContextAcessor is not null)
                {
                    var tokenStore = provider.GetRequiredService<ITokenStore>();

                    var authorization = httpContextAcessor.HttpContext?.Request.Headers["Authorization"];

                    var tokenKey = Guid.Parse(authorization.ToString() ?? throw new Exception("Token Authorization não informado"));

                    var environment = tokenStore.GetEnvironment(tokenKey);

                    return contextFactory.Create(environment, string.Format(connectionString, environment.Database));
                }

                var tokenAcessor = provider.GetRequiredService<TokenAcessor>();

                if (tokenAcessor is not null)
                {
                    return contextFactory.Create(tokenAcessor.AnalysisEnvironment, string.Format(connectionString, tokenAcessor.AnalysisEnvironment.Database));
                }

                return null;
            });
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationModule));
        }
    }
}
