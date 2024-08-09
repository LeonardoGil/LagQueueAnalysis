using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;

namespace LagQueueApplication.Processings
{
    internal class QueueRegisterProcessingEvent : IQueueRegisterProcessingEvent
    {
        public Task Run(QueueRegisterEvent command)
        {
            throw new NotImplementedException();
        }
    }
}
