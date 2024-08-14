using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagRabbitMQ.Interfaces
{
    public interface IQueueRabbitServices
    {
        Task<List<object>> Request();
    }
}
