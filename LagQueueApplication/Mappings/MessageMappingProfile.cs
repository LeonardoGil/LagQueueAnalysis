﻿using AutoMapper;
using LagQueueApplication.Models;
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

            CreateMap<Message, MessageQueryModel>()
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.MessageId))
                .ForMember(dest => dest.Queue, opt => opt.MapFrom(src => src.Queue.Name))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        }
    }
}
