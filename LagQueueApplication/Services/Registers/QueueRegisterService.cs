using LagQueueApplication.Interfaces;
using LagQueueDomain.Settings;

namespace LagQueueApplication.Services.Registers
{
    public class QueueRegisterService : IQueueRegisterService
    {
        public Guid Register(RabbitMQSettings rabbitMQSettings)
        {
            throw new NotImplementedException();
        }

        public Task BackgroundRun()
        {
            throw new NotImplementedException();
        }
    }
}
