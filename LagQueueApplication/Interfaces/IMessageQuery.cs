using LagQueueApplication.Filters;
using LagQueueApplication.Models;

namespace LagQueueApplication.Interfaces
{
    public interface IMessageQuery
    {
        public List<MessageQueryModel> List(MessageListFilter filter);
    }
}
