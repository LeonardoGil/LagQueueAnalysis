using AutoMapper;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Models;
using LagQueueDomain.Entities;

namespace LagQueueApplication.Queries
{
    public class ProcessingEventQuery(IBaseRepository<LagQueueContext> repository,
                                      IMapper mapper) : IProcessingEventQuery
    {
        public ProcessingEventQueryModel GetById(Guid id)
        {
            var processingEvent = repository.Get<ProcessingEvent>().FirstOrDefault(x => x.Id == id) ?? throw new Exception($"ProcessingEventId: '{id}' não encontrado!");

            return mapper.Map<ProcessingEventQueryModel>(processingEvent);
        }

        public IList<ProcessingEventQueryModel> GetLastEvents(int take = 10)
        {
            var processingEvents = repository.Get<ProcessingEvent>().OrderByDescending(x => x.Start).Take(10);

            return mapper.Map<List<ProcessingEventQueryModel>>(processingEvents);
        }
    }
}
