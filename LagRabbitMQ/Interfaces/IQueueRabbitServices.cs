using LagRabbitMQ.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagRabbitMQ.Interfaces
{
    public interface IQueueRabbitServices
    {
        Task<List<QueueDto>> QueueListRequest();

        Task<QueueDto> QueueRequest(string vHost, string queue);

        Task<List<MessageDto>> QueueMessagesGetRequest(string vHost, string queue);

    }
}
