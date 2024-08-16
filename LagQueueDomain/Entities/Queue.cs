using System;
using System.Collections.Generic;

namespace LagQueueDomain.Entities
{
    public class Queue
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Host { get; set; }

        public virtual List<Message> Messages { get; set; }
    }
}
