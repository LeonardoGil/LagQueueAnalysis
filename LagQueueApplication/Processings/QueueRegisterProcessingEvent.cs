using AutoMapper;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using LagQueueDomain.Entities;
using LagRabbitMQ.Interfaces;

namespace LagQueueApplication.Processings
{
    public class QueueRegisterProcessingEvent : IQueueRegisterProcessingEvent
    {
        private readonly IMapper _mapper;
        private readonly IQueueRabbitServices _queueRabbitServices;
        private readonly IQueueService _queueService;

        public QueueRegisterProcessingEvent(IMapper mapper, IQueueRabbitServices queueRabbitServices, IQueueService queueService)
        {
            _mapper = mapper;
            _queueRabbitServices = queueRabbitServices;
            _queueService = queueService;
        }

        public async Task Run(QueueRegisterEvent command)
        {
            var queueDtoList = await _queueRabbitServices.Request();

            var queues = _mapper.Map<List<Queue>>(queueDtoList);

            _queueService.Register(queues);
        }
    }
}
