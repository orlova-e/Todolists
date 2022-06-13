using AutoMapper;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Services.Translation.Option;

public class OptionToOptionEditorDtoMap : Profile
{
    public OptionToOptionEditorDtoMap()
    {
        CreateMap<Domain.Core.Entities.Option, OptionEditorDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Checked, o => o.MapFrom(x => x.Checked))
            .ForMember(x => x.Text, o => o.MapFrom(x => x.Text));
    }
}