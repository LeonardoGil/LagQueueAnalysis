using AutoMapper;
using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueApplication.Filters;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Models;
using LagQueueDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LagQueueApplication.Queries
{
    public class MessageQuery : IMessageQuery
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<LagQueueContext> _baseRepository;

        public MessageQuery(IMapper mapper, IBaseRepository<LagQueueContext> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public List<MessageQueryModel> List(MessageListFilter filter)
        {
            var query = _baseRepository.Get<Message>().Include(x => x.Queue).AsNoTracking();

            if (filter.Queues is not null && filter.Queues.Length > 0)
            {
                query = query.Where(x => filter.Queues.Contains(x.Queue.Name));
            }

            var messages = query.Take(filter.Count).ToList();

            return _mapper.Map<List<MessageQueryModel>>(messages);
        }
    }
}
