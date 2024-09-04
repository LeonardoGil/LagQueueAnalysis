using LagRabbitMQ.Consts;
using LagRabbitMQ.DTOs;
using LagRabbitMQ.Interfaces;
using LagRabbitMQ.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagRabbitMQ.Services
{
    public class QueueRabbitService : IQueueRabbitService
    {
        private readonly RabbitMQSetting _setting;

        public QueueRabbitService(RabbitMQSetting setting)
        {
            _setting = setting;
        }

        public async Task<List<QueueDto>> QueueListRequest()
        {
            var baseUrl = new Uri(_setting.Url);

            var url = new Uri(baseUrl, RabbitUrls.QueueList);

            return await RequestServices.Get<List<QueueDto>>(url, _setting.Token());
        }

        public async Task<List<MessageDto>> QueueMessagesGetRequest(string vHost, string queue, int take = 500)
        {
            var baseUrl = new Uri(_setting.Url);

            var endpoint = string.Format(RabbitUrls.QueueMessagesGet, GetHost(vHost), queue);

            var url = new Uri(baseUrl, endpoint);

            var body = new
            {
                ackmode = "ack_requeue_true",
                encoding = "auto",
                count = take
            };

            return await RequestServices.Post<List<MessageDto>>(url, _setting.Token(), body);
        }

        public async Task<QueueDto> QueueRequest(string vHost, string queue)
        {
            var baseUrl = new Uri(_setting.Url);

            var endpoint = string.Format(RabbitUrls.Queue, GetHost(vHost), queue);

            var url = new Uri(baseUrl, endpoint);

            return await RequestServices.Get<QueueDto>(url, _setting.Token());
        }

        private string GetHost(string host)
        {
            return host.Equals("/") ? RabbitUrls.VHostDefault : host;
        }
    }
}
