namespace LagRabbitMQ.Consts
{
    public static class RabbitUrls
    {
        public const string Overview = "/api/overview";

        public const string Queue = "api/queues/{0}/{1}";
        public const string QueueList = "api/queues";
        public const string QueueMessagesGet = "api/queues/{0}/{1}/get";

        public const string VHostDefault = "%2f";
    }
}
