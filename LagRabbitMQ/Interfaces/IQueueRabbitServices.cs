using LagRabbitMQ.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagRabbitMQ.Interfaces
{
    public interface IQueueRabbitServices
    {
        Task<List<QueueDto>> QueueListRequest();

        Task<List<object>> QueueMessagesGetRequest(string vHost, string queue);
    }
}
