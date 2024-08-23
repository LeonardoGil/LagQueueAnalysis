using AutoMapper;
using LagQueueDomain.Entities;
using LagRabbitMQ.DTOs;

namespace LagQueueApplication.Mappings
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<MessageDto, Message>()
                .ForMember(dest => dest.Expiration, opt => opt.MapFrom(src => src.properties.expiration))
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.properties.message_id))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.message_count))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.properties.type));
        }
    }
}
