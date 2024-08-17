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
            var url = $"http://localhost:15672/{RabbitUrls.QueueList}";

            var authToken = GetAuthToken();

            return await RequestServices.Get<List<QueueDto>>(url, authToken);
        }

        public async Task<List<object>> QueueMessagesGetRequest(string vHost, string queue)
        {
            var url = string.Format($"{RabbitUrls.DefaultUrl}{RabbitUrls.QueueMessagesGet}", vHost, queue);

            var authToken = GetAuthToken();

            throw new NotImplementedException();

            // TODO: Implementar...

            return await RequestServices.Get<List<object>>(url, authToken);
        }


        /// <summary>
        ///     Gera token de autenticação da API do RabbitMQ
        /// </summary>
        private string GetAuthToken(string user = "guest", string password = "guest") => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{password}"));
    }
}
