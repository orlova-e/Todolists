using AutoMapper;
using Todolists.Web.Dtos.Checklist;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Translation.Checklist;

public class ChecklistToChecklistEditorDtoMap : Profile
{
    public ChecklistToChecklistEditorDtoMap()
    {
        CreateMap<Domain.Core.Entities.Checklist, ChecklistEditorDto>()
            .IncludeBase<Domain.Core.Entities.NoteBase, NoteBaseEditorDto>()
            .ForMember(x => x.Options, o => o.MapFrom(x => x.Options));
    }
}