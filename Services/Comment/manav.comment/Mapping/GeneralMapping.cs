using AutoMapper;
using manav.component.Dtos;
using manav.component.Entities;

namespace manav.component.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<UserComment, ResultCommentDto>().ReverseMap();
        CreateMap<UserComment, GetByIdCommentDto>().ReverseMap();
        CreateMap<UserComment, CreateCommentDto>().ReverseMap();
        CreateMap<UserComment, UpdateCommentDto>().ReverseMap();
    }
}