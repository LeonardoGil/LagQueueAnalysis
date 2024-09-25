namespace LagRabbitMQ.DTOs
{
    public class QueueDto
    {
        // Verificar
        public object arguments { get; set; }
        
        public bool auto_delete { get; set; }
        public string consumer_capacity { get; set; }
        public string consumer_utilisation { get; set; }
        public int consumers { get; set; }
        public bool durable { get; set; }

        //  Verificar 
        public object effective_policy_definition { get; set; }

        public bool exclusive { get; set; }
        public int memory { get; set; }
        public int message_bytes { get; set; }
        public int message_bytes_paged_out { get; set; }
        public int message_bytes_persistent { get; set; }
        public int message_bytes_ram { get; set; }
        public int message_bytes_ready { get; set; }
        public int message_bytes_unacknowledged { get; set; }
        public int messages { get; set; }
        public Messages_Details messages_details { get; set; }
        public int messages_paged_out { get; set; }
        public int messages_persistent { get; set; }
        public int messages_ram { get; set; }
        public int messages_ready { get; set; }
        public Messages_Ready_Details messages_ready_details { get; set; }
        public int messages_ready_ram { get; set; }
        public int messages_unacknowledged { get; set; }
        public Messages_Unacknowledged_Details messages_unacknowledged_details { get; set; }
        public int messages_unacknowledged_ram { get; set; }
        public string name { get; set; }
        public string node { get; set; }
        public long reductions { get; set; }
        public Reductions_Details reductions_details { get; set; }
        public string state { get; set; }
        public int storage_version { get; set; }
        public string type { get; set; }
        public string vhost { get; set; }
    }

    public class Messages_Details
    {
        public float rate { get; set; }
    }

    public class Messages_Ready_Details
    {
        public float rate { get; set; }
    }

    public class Messages_Unacknowledged_Details
    {
        public float rate { get; set; }
    }

    public class Reductions_Details
    {
        public float rate { get; set; }
    }
}
