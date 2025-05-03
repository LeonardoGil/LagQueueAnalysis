using AutoMapper;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using LagQueueDomain.Entities;
using LagRabbitMq = LagRabbitMqManagerToolkit.Services.Interfaces;

namespace LagQueueApplication.Processings
{
    public class RegisterQueueProcessingEvent(IMapper mapper, LagRabbitMq.IQueueService queueRabbitService, IQueueService queueService) : IRegisterQueueProcessingEvent
    {
        public async Task Run(RegisterQueueEvent command)
        {
            try
            {
                var queueDtoList = await queueRabbitService.ListAsync();

                var queues = mapper.Map<List<Queue>>(queueDtoList);

                queueService.Register(queues);
            }
            catch (Exception ex)
            {
                // TODO: Pensar numa forma de Tratar...
                throw new Exception($"Ocorreu um erro inesperado no {command}. InnerExceptionMessage: {ex.Message}", ex);
            }
        }
    }
}
