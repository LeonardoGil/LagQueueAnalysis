namespace LagQueueApplication.Filters
{
    public class MessageListFilter
    {
        public string[] Queues { get; set; }

        public int Count { get; set; } = 1000;
    }
}
