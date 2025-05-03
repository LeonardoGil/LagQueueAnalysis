using AutoMapper;
using LagQueueAnalysisInfra.Interfaces;
using LagQueueApplication.Interfaces;
using LagQueueApplication.Processings.Events;
using LagQueueDomain.Entities;
using LagRabbitMq = LagRabbitMqManagerToolkit.Services.Interfaces;

namespace LagQueueApplication.Processings
{
    public class RegisterQueueMessagesProcessingEvent(IMapper mapper,
                                                      LagRabbitMq.IQueueService queueRabbitService,
                                                      IMessageService messageService,
                                                      IQueueRepository queueRepository) : IRegisterQueueMessagesProcessingEvent
    {

        public async Task Run(RegisterQueueMessagesEvent command)
        {
            try
            {
                var queueDto = await queueRabbitService.GetAsync(command.VHost, command.Queue);

                if (queueDto.Messages == 0)
                {
                    // TODO: Log
                    return;
                }

                var queue = queueRepository.GetByName(command.Queue) ?? throw new Exception($"Fila '{command.Queue}' não encontrada");


                // TODO: Estudar forma de consulta as mensagens em lote no Rabbitmq
                var messages = await BatchMessagesRequest(queueDto.Vhost, queueDto.Name, 20000);

                messages.ForEach(m => m.QueueId = queue.Id);

                messageService.Register(messages);
            }
            catch (Exception ex)
            {
                // TODO: Pensar numa forma de Tratar...
                throw new Exception($"Ocorreu um erro inesperado no {nameof(RegisterQueueMessagesProcessingEvent)}. InnerExceptionMessage: {ex.Message}", ex);
            }
        }

        private async Task<List<Message>> BatchMessagesRequest(string vhost, string name, int take)
        {
            var messagesDto = await queueRabbitService.GetMessagesAsync(vhost, name, take);

            return mapper.Map<List<Message>>(messagesDto);
        }
    }
}
