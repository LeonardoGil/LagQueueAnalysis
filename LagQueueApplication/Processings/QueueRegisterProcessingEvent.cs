using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;

namespace LagQueueApplication.Processings
{
    public class QueueRegisterProcessingEvent : IQueueRegisterProcessingEvent
    {
        public async Task Run(QueueRegisterEvent command)
        {
            throw new NotImplementedException();
        }
    }
}
