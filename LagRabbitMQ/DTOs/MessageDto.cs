﻿using Newtonsoft.Json;

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
        [JsonProperty("$.diagnostics.hostdisplayname")]
        public string diagnosticshostdisplayname { get; set; }

        [JsonProperty("$.diagnostics.hostid")]
        public string diagnosticshostid { get; set; }

        [JsonProperty("$.diagnostics.originating.hostid")]
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

        #region Exception 
        [JsonProperty("NServiceBus.ExceptionInfo.Data.Handler failure time")]
        public string NServiceBusExceptionInfoDataHandlerFailureTime { get; set; }
        public string NServiceBusExceptionInfoDataHandlerStartTime { get; set; }
        public string NServiceBusExceptionInfoDataHandlerType { get; set; }
        public string NServiceBusExceptionInfoDataMessageID { get; set; }
        public string NServiceBusExceptionInfoDataMessageType { get; set; }

        [JsonProperty("NServiceBus.ExceptionInfo.ExceptionType")]
        public string NServiceBusExceptionInfoExceptionType { get; set; }
        public string NServiceBusExceptionInfoHelpLink { get; set; }

        [JsonProperty("NServiceBus.ExceptionInfo.Message")] 
        public string NServiceBusExceptionInfoMessage { get; set;    }
        public string NServiceBusExceptionInfoSource { get; set; }

        [JsonProperty("NServiceBus.ExceptionInfo.StackTrace")] 
        public string NServiceBusExceptionInfoStackTrace { get; set; }
        #endregion

        [JsonProperty("NServiceBus.ProcessingEnded")]
        public string NServiceBusProcessingEnded { get; set; }
        public string NServiceBusProcessingEndpoint { get; set; }
        public string NServiceBusProcessingMachine { get; set; }

        [JsonProperty("NServiceBus.ProcessingStarted")]
        public string NServiceBusProcessingStarted { get; set; }
        public string NServiceBusReplyToAddress { get; set; }

        [JsonProperty("NServiceBus.TimeSent")]
        public string NServiceBusTimeSent { get; set; }
        public string NServiceBusTransportRabbitMQConfirmationId { get; set; }
        public string NServiceBusVersion { get; set; }

        public string TraceId { get; set; }
        public string NServiceBusRelatedTo { get; set; }
        public string Tenant { get; set; }
    }
}
