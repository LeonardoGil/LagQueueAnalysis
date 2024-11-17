using AutoMapper;
using LagQueueApplication.Models;
using LagQueueDomain.Entities;

namespace LagQueueApplication.Mappings
{
    public class ProcessingEventMappingProfile : Profile
    {
        public ProcessingEventMappingProfile()
        {
            CreateMap<ProcessingEvent, ProcessingEventQueryModel>()
                .ReverseMap();
        }
    }
}