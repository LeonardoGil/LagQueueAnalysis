using LagRabbitMQ.Consts;
using LagRabbitMQ.Interfaces;
using LagRabbitMQ.Settings;
using System;
using System.Threading.Tasks;

namespace LagRabbitMQ.Services
{
    public class OverviewRabbitService : IOverviewRabbitService
    {
        public async Task OverviewRequest(RabbitMQSetting setting)
        {
            var baseUrl = new Uri(setting.Url);

            var url = new Uri(baseUrl, RabbitUrls.Overview);

            await RequestServices.Get<object>(url, setting.Token());
        }
    }
}
