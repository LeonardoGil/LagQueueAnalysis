using LagRabbitMQ.Settings;
using System.Threading.Tasks;

namespace LagRabbitMQ.Interfaces
{
    public interface IOverviewRabbitService
    {
        Task OverviewRequest(RabbitMQSetting setting); 
    }
}
