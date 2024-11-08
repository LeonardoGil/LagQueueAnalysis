using System;

namespace LagQueueDomain.Entities
{
    public class ExceptionMessage
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string Type { get; set; }

        public DateTime TimeOfFailure { get; set; }
    }
}
