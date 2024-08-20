using LagQueueApplication.Interfaces;
using LagQueueDomain.Entities;
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

        public Task<Guid> On<Event, ProcessingEventService>(Event obj, string serviceName = nameof(ExecuteProcessingEventService)) where ProcessingEventService : IProcessingEvent<Event>
        {
            ProcessingEvent processingEvent = _processingEventService.Register(serviceName);

            _ = Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                
                var processingEventService = scope.ServiceProvider.GetRequiredService<IProcessingEventService>();

                try
                {
                    var service = scope.ServiceProvider.GetRequiredService<ProcessingEventService>();

                    await service.Run(obj);

                    processingEventService.ProcessSuccess(processingEvent);
                }
                catch (Exception exception)
                {
                    processingEventService.ProcessFail(processingEvent, exception);
                }
            });

            return Task.FromResult(processingEvent.Id);
        }
    }
}
