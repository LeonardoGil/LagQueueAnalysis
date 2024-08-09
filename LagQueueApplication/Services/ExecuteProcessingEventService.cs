using LagQueueApplication.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LagQueueApplication.Services
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

        public Task<Guid> On<Event, ProcessingEvent>(Event obj, string serviceName = nameof(ExecuteProcessingEventService)) where ProcessingEvent : IProcessingEvent<Event>
        {
            var processingId = _processingEventService.Register(serviceName);

            Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();

                var service = scope.ServiceProvider.GetRequiredService<ProcessingEvent>();

                await service.Run(obj);
            });

            return Task.FromResult(processingId);
        }
    }
}
