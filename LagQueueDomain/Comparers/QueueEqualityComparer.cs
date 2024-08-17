using LagQueueDomain.Entities;
using System;
using System.Collections.Generic;

namespace LagQueueDomain.Comparers
{
    public class QueueEqualityComparer : IEqualityComparer<Queue>
    {
        public static QueueEqualityComparer Create() => new QueueEqualityComparer();

        public bool Equals(Queue x, Queue y)
        {
            if (x.Id != Guid.Empty &&
                y.Id != Guid.Empty)
            {
                return x.Id == y.Id;
            }

            if (x.Host == y.Host &&
                x.Name == y.Name)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(Queue obj)
        {
            if (obj is null) 
                return 0;

            return obj.Name.GetHashCode() ^ obj.Host.GetHashCode();
        }
    }
}
