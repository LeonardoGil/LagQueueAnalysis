using LagQueueAnalysisInfra.Interfaces;
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

        public void ProcessFail(ProcessingEvent processingEvent, Exception exception)
        {
            processingEvent.SetFail(exception);

            if (!_repository.IsTracking(processingEvent))
            {
                _repository.Update(processingEvent);
            }

            _repository.SaveChanges();
        }

        public void ProcessSuccess(ProcessingEvent processingEvent)
        {
            processingEvent.SetSucess();

            if (!_repository.IsTracking(processingEvent))
            {
                _repository.Update(processingEvent);
            }

            _repository.SaveChanges();
        }

        public ProcessingEvent Register(string name)
        {
            var processingEvent = ProcessingEvent.Create(name);

            _repository.Add(processingEvent);
            _repository.SaveChanges();

            return processingEvent;
        }
    }
}
