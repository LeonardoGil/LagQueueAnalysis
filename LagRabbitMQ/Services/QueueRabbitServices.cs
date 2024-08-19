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

            var authToken = GetAuthToken();

            return await RequestServices.Get<List<QueueDto>>(url, authToken);
        }

        public async Task<List<MessageDto>> QueueMessagesGetRequest(string vHost, string queue)
        {
            if (vHost == "/")
                vHost = RabbitUrls.VHostDefault;

            var url = string.Format($"{RabbitUrls.DefaultUrl}{RabbitUrls.QueueMessagesGet}", vHost, queue);

            var authToken = GetAuthToken();

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

            var authToken = GetAuthToken();

            return await RequestServices.Get<QueueDto>(url, authToken);
        }

        /// <summary>
        ///     Gera token de autenticação da API do RabbitMQ
        /// </summary>
        private string GetAuthToken(string user = "guest", string password = "guest") => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{password}"));
    }
}
