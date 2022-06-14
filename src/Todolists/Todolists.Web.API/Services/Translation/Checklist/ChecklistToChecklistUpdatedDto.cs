using AutoMapper;
using Todolists.Services.Messaging.Models.Checklists;

namespace Todolists.Web.API.Services.Translation.Checklist;

public class ChecklistToChecklistUpdatedDto : Profile
{
    public ChecklistToChecklistUpdatedDto()
    {
        CreateMap<Domain.Core.Entities.Checklist, ChecklistUpdatedDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Updated, o => o.MapFrom(x => (DateTime) x.Updated))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.UserId, o => o.MapFrom(x => x.UserId))
            .ForMember(x => x.Options, o => o.MapFrom(x => x.Options));
    }
}