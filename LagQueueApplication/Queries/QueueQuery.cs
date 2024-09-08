using AutoMapper;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Models;
using LagQueueDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LagQueueApplication.Queries
{
    public class QueueQuery : IQueueQuery
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<LagQueueContext> _baseRepository;

        public QueueQuery(IMapper mapper, IBaseRepository<LagQueueContext> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public List<QueueQueryModel> List()
        {
            var queues = _baseRepository.Get<Queue>().AsNoTracking().ToList();

            return _mapper.Map<List<QueueQueryModel>>(queues);
        }
    }
}
