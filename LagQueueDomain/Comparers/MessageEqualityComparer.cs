using LagQueueDomain.Entities;
using System.Collections.Generic;

namespace LagQueueDomain.Comparers
{
    public class MessageEqualityComparer : IEqualityComparer<Message>
    {
        public bool Equals(Message x, Message y)
        {
            return x.MessageId == y.MessageId;
        }

        public int GetHashCode(Message obj)
        {
            if (obj is null)
                return 0;

            return obj.MessageId.GetHashCode();
        }
    }
}
