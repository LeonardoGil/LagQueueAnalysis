using LagRabbitMQ.Settings;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LagEnvironmentDomain.Entities
{
    public class AnalysisEnvironment
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string Database { get; set; }

        [NotMapped]
        public RabbitMQSetting RabbitMQSetting { get; set; }
    }
}
