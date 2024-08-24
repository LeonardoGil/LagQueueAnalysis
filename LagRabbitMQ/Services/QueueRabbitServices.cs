using LagRabbitMQ.Consts;
using LagRabbitMQ.DTOs;
using LagRabbitMQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LagRabbitMQ.Services
{
    public class QueueRabbitServices : IQueueRabbitServices
    {
        public async Task<List<QueueDto>> QueueListRequest()
        {
            var baseUrl = new Uri(RabbitUrls.DefaultUrl);

            var url = new Uri(baseUrl, RabbitUrls.QueueList);

            var authToken = RequestServices.GetAuthToken("guest", "guest");

            return await RequestServices.Get<List<QueueDto>>(url, authToken);
        }

        public async Task<List<MessageDto>> QueueMessagesGetRequest(string vHost, string queue, int take = 500)
        {
            var baseUrl = new Uri(RabbitUrls.DefaultUrl);

            var endpoint = string.Format(RabbitUrls.QueueMessagesGet, GetHost(vHost), queue);

            var url = new Uri(baseUrl, endpoint);

            var authToken = RequestServices.GetAuthToken("guest", "guest");

            var body = new
            {
                ackmode = "ack_requeue_true",
                encoding = "auto",
                count = take
            };

            return await RequestServices.Post<List<MessageDto>>(url, authToken, body);
        }

        public async Task<QueueDto> QueueRequest(string vHost, string queue)
        {
            var baseUrl = new Uri(RabbitUrls.DefaultUrl);

            var endpoint = string.Format(RabbitUrls.Queue, GetHost(vHost), queue);

            var url = new Uri(baseUrl, endpoint);

            var authToken = RequestServices.GetAuthToken("guest", "guest");

            return await RequestServices.Get<QueueDto>(url, authToken);
        }

        private string GetHost(string host)
        {
            return host.Equals("/") ? RabbitUrls.VHostDefault : host;
        }
    }
}
