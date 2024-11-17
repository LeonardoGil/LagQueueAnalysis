using LagQueueDomain.Enums;

namespace LagQueueApplication.Models
{
    public class ProcessingEventQueryModel
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public ProcessingEventStatusEnum Status { get; set; }

        public string Exception { get; set; }
    }
}
