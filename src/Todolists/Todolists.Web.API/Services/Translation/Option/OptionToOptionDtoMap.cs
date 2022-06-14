using AutoMapper;
using Todolists.Services.Messaging.Models.Options;

namespace Todolists.Web.API.Services.Translation.Option;

public class OptionToOptionDtoMap : Profile
{
    public OptionToOptionDtoMap()
    {
        CreateMap<Domain.Core.Entities.Option, OptionDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Checked, o => o.MapFrom(x => x.Checked))
            .ForMember(x => x.Text, o => o.MapFrom(x => x.Text));
    }
}