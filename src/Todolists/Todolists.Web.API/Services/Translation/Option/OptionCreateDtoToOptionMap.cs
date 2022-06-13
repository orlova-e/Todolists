using AutoMapper;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Services.Translation.Option;

public class OptionCreateDtoToOptionMap : Profile
{
    public OptionCreateDtoToOptionMap()
    {
        CreateMap<OptionCreateDto, Domain.Core.Entities.Option>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.IsDeleted, o => o.Ignore())
            .ForMember(x => x.Checked, o => o.MapFrom(x => x.Checked))
            .ForMember(x => x.Text, o => o.MapFrom(x => x.Text));
    }
}