using AutoMapper;
using LagQueueApplication.Models;
using LagQueueDomain.Entities;
using LagRabbitMQ = LagRabbitMqManagerToolkit.Requests;

namespace LagQueueApplication.Mappings
{
    public class QueueMappingProfile : Profile
    {
        public QueueMappingProfile()
        {
            CreateMap<LagRabbitMQ.Queue, Queue>()
                .ForMember(dest => dest.Name, src => src.MapFrom(dto => dto.Name))
                .ForMember(dest => dest.Host, src => src.MapFrom(dto => dto.Vhost));

            CreateMap<Queue, QueueQueryModel>();
        }
    }
}
