using AutoMapper;
using Todolists.Services.Messaging.Models.Checklists;

namespace Todolists.Web.API.Services.Translation.Checklist;

public class ChecklistToChecklistCreatedDto : Profile
{
    public ChecklistToChecklistCreatedDto()
    {
        CreateMap<Domain.Core.Entities.Checklist, ChecklistCreatedDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Created, o => o.MapFrom(x => x.Created))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.UserId, o => o.MapFrom(x => x.UserId))
            .ForMember(x => x.Options, o => o.MapFrom(x => x.Options));
    }
}