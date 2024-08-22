namespace LagQueueApplication.Processings.Events
{
    public class RegisterQueueMessagesEvent
    {
        public RegisterQueueMessagesEvent(string queueName, string vhost)
        {
            Queue = queueName;
            VHost = vhost;
        }

        public string VHost { get; private set; }

        public string Queue { get; private set; }
    }
}
