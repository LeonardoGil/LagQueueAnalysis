using AutoMapper;
using LagQueueDomain.Entities;
using LagRabbitMQ = LagRabbitMqManagerToolkit.Requests;

namespace LagQueueApplication.Mappings.MappingResolvers
{
    public class ExceptionMessageMappingResolver(IMapper mapper) : IValueResolver<LagRabbitMQ.Message, Message, ExceptionMessage>
    {
        public ExceptionMessage? Resolve(LagRabbitMQ.Message source, Message destination, ExceptionMessage destMember, ResolutionContext context)
        {
            return default;

            // TO DO: Requer headers referente NServiceBus

            //var header = source.Properties?.Headers;

            //if (header is null)
            //    return null;

            //if (string.IsNullOrEmpty(header.NServiceBusExceptionInfoExceptionType) || string.IsNullOrEmpty(header.NServiceBusExceptionInfoMessage))
            //    return null;

            //return mapper.Map<ExceptionMessage>(source);
        }
    }
}
