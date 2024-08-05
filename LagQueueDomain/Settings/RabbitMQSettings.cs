using System;
using System.Text;

namespace LagQueueDomain.Settings
{
    public class RabbitMQSettings
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
