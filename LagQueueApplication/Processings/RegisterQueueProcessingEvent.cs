using AutoMapper;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using LagQueueDomain.Entities;
using LagRabbitMQ.Interfaces;

namespace LagQueueApplication.Processings
{
    public class RegisterQueueProcessingEvent : IRegisterQueueProcessingEvent
    {
        private readonly IMapper _mapper;
        private readonly IQueueRabbitServices _queueRabbitServices;
        private readonly IQueueService _queueService;

        public RegisterQueueProcessingEvent(IMapper mapper, IQueueRabbitServices queueRabbitServices, IQueueService queueService)
        {
            _mapper = mapper;
            _queueRabbitServices = queueRabbitServices;
            _queueService = queueService;
        }

        public async Task Run(RegisterQueueEvent command)
        {
            var queueDtoList = await _queueRabbitServices.Request();

            var queues = _mapper.Map<List<Queue>>(queueDtoList);

            _queueService.Register(queues);
        }
    }
}
