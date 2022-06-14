using AutoMapper;
using Todolists.Services.Messaging.Models.Checklists;

namespace Todolists.Web.API.Services.Translation.Checklist;

public class ChecklistToChecklistDeletedDto : Profile
{
    public ChecklistToChecklistDeletedDto()
    {
        CreateMap<Domain.Core.Entities.Checklist, ChecklistDeletedDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Deleted, o => o.MapFrom(x => (DateTime) x.Deleted));
    }
}