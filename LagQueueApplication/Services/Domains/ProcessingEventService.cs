using LagQueueApplication.Interfaces;
using LagQueueDomain.Entities;

namespace LagQueueApplication.Services.Domains
{
    public class ProcessingEventService : IProcessingEventService
    {
        private readonly IBaseRepository _repository;

        public ProcessingEventService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public Guid Register(string name)
        {
            var processingEvent = ProcessingEvent.Create(name);

            _repository.Add(processingEvent);
            _repository.SaveChanges();

            return processingEvent.Id;
        }
    }
}
