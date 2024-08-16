using AutoMapper;
using LagQueueDomain.Entities;
using LagRabbitMQ.DTOs;

namespace LagQueueApplication.Mappings
{
    public class QueueMappingProfile : Profile
    {
        public QueueMappingProfile()
        {
            CreateMap<QueueDto, Queue>()
                .ForMember(dest => dest.Name, src => src.MapFrom(dto => dto.name))
                .ForMember(dest => dest.Host, src => src.MapFrom(dto => dto.vhost));
        }
    }
}
