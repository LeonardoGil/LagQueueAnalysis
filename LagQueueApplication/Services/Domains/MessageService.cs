using LagQueueAnalysisInfra.Interfaces;
using LagQueueApplication.Interfaces;
using LagQueueDomain.Comparers;
using LagQueueDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LagQueueApplication.Services.Domains
{
    public class MessageService : IMessageService
    {
        private readonly IBaseRepository _repository;

        public MessageService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public void Register(List<Message> messages)
        {
            var messagesId = messages.Select(x => x.MessageId);

            var messagesUpdates = _repository.Get<Message>().Where(entity => messagesId.Contains(entity.MessageId)).AsNoTracking().ToList();

            var messagesAdds = messages.Except(messagesUpdates, new MessageEqualityComparer()).ToList();
            
            // No momento não há necessidade de atualizar as Messages
            // verificar a necessidade no Futuro...

            if (messagesAdds.Count == 0)
                return;

            _repository.AddRange(messagesAdds);
            _repository.SaveChanges();
        }
    }
}
