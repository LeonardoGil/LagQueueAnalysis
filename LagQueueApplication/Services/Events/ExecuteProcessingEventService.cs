using LagQueueApplication.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LagQueueApplication.Services.Events
{
    public class ExecuteProcessingEventService : IExecuteProcessingEventService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IProcessingEventService _processingEventService;

        public ExecuteProcessingEventService(IServiceScopeFactory scopeFactory, IProcessingEventService processingEventService)
        {
            _scopeFactory = scopeFactory;
            _processingEventService = processingEventService;
        }


        public Task<Guid> On<Command, TBackgroundService>(Command obj, string serviceName = nameof(ExecuteProcessingEventService)) where TBackgroundService : IBackgroundService
        {
            var processingId = _processingEventService.Register(serviceName);

            Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                
                var service = scope.ServiceProvider.GetRequiredService<TBackgroundService>();

                throw new NotImplementedException();
            });

            return Task.FromResult(processingId);
        }
    }
}
