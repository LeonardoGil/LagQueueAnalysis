﻿using System;

namespace LagQueueDomain.Entities
{
    public class Message
    {
        public string Type { get; set; }

        public int Position { get; set; }

        public Guid MessageId { get; set; }

        public string Payload { get; set; }

        public uint Expiration { get; set; }

        
        public Guid QueueId { get; set; }
        public virtual Queue Queue { get; set; }


        public Guid? ReplyToId { get; set; }
        public virtual Queue ReplyTo { get; set; }

        public DateTime ProcessingStarted { get; set; }

        public DateTime ProcessingEnded { get; set; }
    }
}
