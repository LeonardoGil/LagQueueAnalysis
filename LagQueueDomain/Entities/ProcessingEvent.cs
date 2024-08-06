using LagQueueDomain.Enums;
using System;

namespace LagQueueDomain.Entities
{
    public class ProcessingEvent
    {
        public ProcessingEvent() { }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public ProcessingEventStatusEnum Status { get; private set; }

        public string Exception { get; private set; }

        public void SetSucess()
        {
            Status = ProcessingEventStatusEnum.Sucess;
            End = DateTime.UtcNow;
        }

        public void SetFail(Exception exception)
        {
            Status = ProcessingEventStatusEnum.Fail;
            Exception = exception.Message;
            End = DateTime.UtcNow;
        }

        public static ProcessingEvent Create(string name)
        {
            return new ProcessingEvent()
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }
    }
}
