using AutoMapper;
using manav.Message.Data_Access_Layer.Entities;
using manav.Message.Dtos;

namespace manav.Message.Mapping;

public class GeneralMapping:Profile
{
    public GeneralMapping()
    {
        CreateMap<UserMessage, CreateMessageDto>().ReverseMap();
        CreateMap<UserMessage, GetByIdMesageDto>().ReverseMap();
        CreateMap<UserMessage, ResultInboxMessageDto>().ReverseMap();
        CreateMap<UserMessage, ResultMessageDto>().ReverseMap();
        CreateMap<UserMessage, ResultSendboxMessageDto>().ReverseMap();
        CreateMap<UserMessage, UpdateMessageDto>().ReverseMap();


    }
}