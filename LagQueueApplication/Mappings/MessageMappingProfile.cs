using AutoMapper;
using LagQueueApplication.Mappings.MappingResolvers;
using LagQueueApplication.Models;
using LagQueueDomain.Entities;
using LagRabbitMQ = LagRabbitMqManagerToolkit.Requests;

namespace LagQueueApplication.Mappings
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<LagRabbitMQ.Message, Message>()
                .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.Properties.Expiration))
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.Properties.MessageId))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.MessageCount))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Properties.Type))
                //.ForMember(dest => dest.ProcessingStarted, opt => opt.MapFrom(src => DateTime.ParseExact(src.Properties.Headers.NServiceBusProcessingStarted, "yyyy-MM-dd HH:mm:ss:ffffff 'Z'", System.Globalization.CultureInfo.InvariantCulture)))
                //.ForMember(dest => dest.ProcessingEnded, opt => opt.MapFrom(src => DateTime.ParseExact(src.Properties.Headers.NServiceBusProcessingEnded, "yyyy-MM-dd HH:mm:ss:ffffff 'Z'", System.Globalization.CultureInfo.InvariantCulture)))
                //.ForMember(dest => dest.TimeSent, opt => opt.MapFrom(src => DateTime.ParseExact(src.Properties.Headers.NServiceBusTimeSent, "yyyy-MM-dd HH:mm:ss:ffffff 'Z'", System.Globalization.CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Exception, opt => opt.MapFrom<ExceptionMessageMappingResolver>());

            CreateMap<LagRabbitMQ.Message, ExceptionMessage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
                //.ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Properties.Headers.NServiceBusExceptionInfoMessage))
                //.ForMember(dest => dest.StackTrace, opt => opt.MapFrom(src => src.Properties.Headers.NServiceBusExceptionInfoStackTrace))
                //.ForMember(dest => dest.TimeOfFailure, opt => opt.MapFrom(src => DateTime.Parse(src.Properties.Headers.NServiceBusExceptionInfoDataHandlerFailureTime)))
                //.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Properties.Headers.NServiceBusExceptionInfoExceptionType));

            CreateMap<Message, MessageQueryModel>()
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
                .ForMember(dest => dest.Queue, opt => opt.MapFrom(src => src.Queue.Name))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
        }
    }
}
