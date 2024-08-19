namespace LagRabbitMQ.DTOs
{
    public class MessageDto
    { 
        public int payload_bytes { get; set; }
        public bool redelivered { get; set; }
        public string exchange { get; set; }
        public string routing_key { get; set; }
        public int message_count { get; set; }
        public Properties properties { get; set; }
        public string payload { get; set; }
        public string payload_encoding { get; set; }
    }

    public class Properties
    {
        public string type { get; set; }
        public string message_id { get; set; }
        public string expiration { get; set; }
        public string reply_to { get; set; }
        public string correlation_id { get; set; }
        public int delivery_mode { get; set; }
        public Headers headers { get; set; }
        public string content_type { get; set; }
    }

    public class Headers
    {
        public string diagnosticshostdisplayname { get; set; }
        public string diagnosticshostid { get; set; }
        public string diagnosticsoriginatinghostid { get; set; }
        public string NServiceBusContentType { get; set; }
        public string NServiceBusConversationId { get; set; }
        public string NServiceBusCorrelationId { get; set; }
        public string NServiceBusEnclosedMessageTypes { get; set; }
        public string NServiceBusMessageId { get; set; }
        public string NServiceBusMessageIntent { get; set; }
        public string NServiceBusNonDurableMessage { get; set; }
        public string NServiceBusOriginatingEndpoint { get; set; }
        public string NServiceBusOriginatingMachine { get; set; }
        public string NServiceBusProcessingEnded { get; set; }
        public string NServiceBusProcessingEndpoint { get; set; }
        public string NServiceBusProcessingMachine { get; set; }
        public string NServiceBusProcessingStarted { get; set; }
        public string NServiceBusReplyToAddress { get; set; }
        public string NServiceBusTimeSent { get; set; }
        public string NServiceBusTransportRabbitMQConfirmationId { get; set; }
        public string NServiceBusVersion { get; set; }
        public string TraceId { get; set; }
        public string NServiceBusRelatedTo { get; set; }
        public string Tenant { get; set; }
    }
}
