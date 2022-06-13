using AutoMapper;
using Todolists.Web.Dtos.Checklist;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Translation.Checklist;

public class ChecklistToChecklistGetDtoMap : Profile
{
    public ChecklistToChecklistGetDtoMap()
    {
        CreateMap<Domain.Core.Entities.Checklist, ChecklistGetDto>()
            .IncludeBase<Domain.Core.Entities.NoteBase, NoteBaseGetDto>()
            .ForMember(x => x.Options, o => o.MapFrom(x => x.Options));
    }
}