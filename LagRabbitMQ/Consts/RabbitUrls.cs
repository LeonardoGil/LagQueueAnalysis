namespace LagRabbitMQ.Consts
{
    public static class RabbitUrls
    {
        public const string DefaultUrl = "http://localhost:15672/";



        public const string QueueList = "api/queues";

        public const string QueueMessagesGet = "api/queues/{0}/{1}/get";
    }
}
