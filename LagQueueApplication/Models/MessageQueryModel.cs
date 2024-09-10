namespace LagQueueApplication.Models
{
    public class MessageQueryModel
    {
        public string Type { get; set; }

        public int Position { get; set; }

        public Guid MessageId { get; set; }

        public string Queue { get; set; }
    }
}
