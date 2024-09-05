using AutoMapper;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using LagQueueDomain.Entities;
using LagRabbitMQ.Interfaces;

namespace LagQueueApplication.Processings
{
    public class RegisterQueueMessagesProcessingEvent : IRegisterQueueMessagesProcessingEvent
    {
        private readonly IMapper _mapper;
        private readonly IQueueRabbitService _queueRabbitService;
        private readonly IMessageService _messageService;
        private readonly IQueueRepository _queueRepository;

        public RegisterQueueMessagesProcessingEvent(IMapper mapper, 
                                                    IQueueRabbitService queueRabbitService, 
                                                    IMessageService messageService, 
                                                    IQueueRepository queueRepository)
        {
            _mapper = mapper;
            _queueRabbitService = queueRabbitService;
            _messageService = messageService;
            _queueRepository = queueRepository;
        }

        public async Task Run(RegisterQueueMessagesEvent command)
        {
            try
            {
                var queueDto = await _queueRabbitService.QueueRequest(command.VHost, command.Queue);

                if (queueDto.messages == 0)
                {
                    // TODO: Log
                    return;
                }

                var queue = _queueRepository.GetByName(command.Queue) ?? throw new Exception($"Fila '{command.Queue}' não encontrada");
                
                var take = 2000;
                var batch = (queueDto.messages / take) + 1;

                for (int i = 0; i < batch; i++)
                {
                    var messages = await BatchMessagesRequest(queueDto.vhost, queueDto.name, take);

                    messages.ForEach(m => m.QueueId = queue.Id);

                    _messageService.Register(messages);
                }
            }
            catch (Exception ex)
            {
                // TODO: Pensar numa forma de Tratar...
                throw new Exception($"Ocorreu um erro inesperado no {nameof(RegisterQueueMessagesProcessingEvent)}. InnerExceptionMessage: {ex.Message}", ex);
            }
        }

        private async Task<List<Message>> BatchMessagesRequest(string vhost, string name, int take)
        {
            //TODO: Adicionar parametro Take
            var messagesDto = await _queueRabbitService.QueueMessagesGetRequest(vhost, name, take);

            return _mapper.Map<List<Message>>(messagesDto);
        }
    }
}
