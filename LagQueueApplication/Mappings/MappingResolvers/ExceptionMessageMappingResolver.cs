using AutoMapper;
using LagQueueDomain.Entities;
using LagRabbitMQ.DTOs;

namespace LagQueueApplication.Mappings.MappingResolvers
{
    public class ExceptionMessageMappingResolver(IMapper mapper) : IValueResolver<MessageDto, Message, ExceptionMessage>
    {
        public ExceptionMessage? Resolve(MessageDto source, Message destination, ExceptionMessage destMember, ResolutionContext context)
        {
            var header = source.properties.headers;

            if (string.IsNullOrEmpty(header.NServiceBusExceptionInfoExceptionType) || string.IsNullOrEmpty(header.NServiceBusExceptionInfoMessage))
                return null;

            return mapper.Map<ExceptionMessage>(source);
        }
    }
}
