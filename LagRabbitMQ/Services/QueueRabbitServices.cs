using LagRabbitMQ.Consts;
using LagRabbitMQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagRabbitMQ.Services
{
    public class QueueRabbitServices : IQueueRabbitServices
    {
        public async Task<List<object>> Request()
        {
            var url = $"http://localhost:15672/{RabbitUrls.QueueList}";

            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"guest:guest"));

            var query = await RequestServices.Get<Object>(url, authToken);

            return default;
        }
    }
}
