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
        private readonly IQueueRabbitService _queueRabbitServices;
        private readonly IQueueService _queueService;

        public RegisterQueueProcessingEvent(IMapper mapper, IQueueRabbitService queueRabbitServices, IQueueService queueService)
        {
            _mapper = mapper;
            _queueRabbitServices = queueRabbitServices;
            _queueService = queueService;
        }

        public async Task Run(RegisterQueueEvent command)
        {
            try
            {
                var queueDtoList = await _queueRabbitServices.QueueListRequest();

                var queues = _mapper.Map<List<Queue>>(queueDtoList);

                _queueService.Register(queues);
            }
            catch (Exception ex)
            {
                // TODO: Pensar numa forma de Tratar...
                throw new Exception($"Ocorreu um erro inesperado no {command}. InnerExceptionMessage: {ex.Message}", ex);
            }
        }
    }
}
