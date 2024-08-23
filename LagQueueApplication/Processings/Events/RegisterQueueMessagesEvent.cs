namespace LagQueueApplication.Processings.Events
{
    public class RegisterQueueMessagesEvent
    {
        public string VHost { get; set; }

        public string Queue { get; set; }
    }
}
