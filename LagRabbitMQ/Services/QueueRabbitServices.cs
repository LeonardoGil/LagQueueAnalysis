using LagRabbitMQ.Consts;
using LagRabbitMQ.DTOs;
using LagRabbitMQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagRabbitMQ.Services
{
    public class QueueRabbitServices : IQueueRabbitServices
    {
        public async Task<List<QueueDto>> QueueListRequest()
        {
            var url = $"{RabbitUrls.DefaultUrl}{RabbitUrls.QueueList}";

            var authToken = RequestServices.GetAuthToken("guest", "guest");

            return await RequestServices.Get<List<QueueDto>>(url, authToken);
        }

        public async Task<List<MessageDto>> QueueMessagesGetRequest(string vHost, string queue)
        {
            if (vHost == "/")
                vHost = RabbitUrls.VHostDefault;

            var url = string.Format($"{RabbitUrls.DefaultUrl}{RabbitUrls.QueueMessagesGet}", vHost, queue);

            var authToken = RequestServices.GetAuthToken("guest", "guest");

            var body = new
            {
                ackmode = "ack_requeue_true",
                encoding = "auto",
                count = 500
            };

            return await RequestServices.Post<List<MessageDto>>(url, authToken, body);
        }

        public async Task<QueueDto> QueueRequest(string vHost, string queue)
        {
            if (vHost == "/")
                vHost = RabbitUrls.VHostDefault;

            var url = string.Format($"{RabbitUrls.DefaultUrl}{RabbitUrls.Queue}", vHost, queue);

            var authToken = RequestServices.GetAuthToken("guest", "guest");

            return await RequestServices.Get<QueueDto>(url, authToken);
        }
    }
}
