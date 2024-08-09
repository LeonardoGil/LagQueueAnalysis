using LagQueueDomain.Settings;

namespace LagQueueApplication.Interfaces
{
    public interface IQueueRegisterService
    {
        Guid Register(RabbitMQSettings rabbitMQSettings);
    }
}
