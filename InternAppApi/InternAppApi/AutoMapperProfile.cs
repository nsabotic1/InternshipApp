using AutoMapper;
using InternAppApi.Dtos.ApplicationCommentDtos;
using InternAppApi.Dtos.ApplicationDtos;
using InternAppApi.Dtos.SelectionComment;
using InternAppApi.Dtos.SelectionDtos;


namespace InternAppApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Application, GetApplicationDto>();
            CreateMap<AddApplicationDto, Application>();

            CreateMap<Selection, GetSelectionDto>();
            CreateMap<Selection, GetSelectionDetailsDto>();
            CreateMap<AddSelectionDto, Selection>();

            CreateMap<UpdateSelectionDto, Selection>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ApplicationComment, GetCommentDto>();
            CreateMap<AddCommentDto, ApplicationComment>();

            CreateMap<SelectionComment, GetSelectionCommentDto>();
            CreateMap<AddSelectionCommentDto, SelectionComment>();

            CreateMap<UpdateSelectionDto, Selection>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}