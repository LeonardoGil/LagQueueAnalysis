using System;
using System.Text;

namespace LagRabbitMQ.Settings
{
    public class RabbitMQSetting
    {
        public string Url { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Token()
        {
            var pass = $"{Username}:{Password}";

            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes(pass));

            return $"Basic {token}";
        }
    }
}
