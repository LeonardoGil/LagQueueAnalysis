using AutoMapper;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using LagRabbitMQ.Interfaces;

namespace LagQueueApplication.Processings
{
    public class RegisterQueueMessagesProcessingEvent : IRegisterQueueMessagesProcessingEvent
    {
        private readonly IMapper _mapper;
        private readonly IQueueRabbitServices _queueRabbitServices;

        public RegisterQueueMessagesProcessingEvent(IMapper mapper, IQueueRabbitServices queueRabbitServices)
        {
            _mapper = mapper;
            _queueRabbitServices = queueRabbitServices;
        }

        public async Task Run(RegisterQueueMessagesEvent command)
        {
            try
            {
                var queueDto = await _queueRabbitServices.QueueRequest(command.VHost, command.Queue);

                var queues = _mapper.Map<List<object>>(queueDto);
            }
            catch (Exception ex)
            {
                // TODO: Pensar numa forma de Tratar...
                throw new Exception($"Ocorreu um erro inesperado no {nameof(RegisterQueueMessagesProcessingEvent)}. InnerExceptionMessage: {ex.Message}", ex);
            }
        }
    }
}
