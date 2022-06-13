using AutoMapper;
using Todolists.Web.Dtos.Checklist;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Translation.Checklist;

public class ChecklistEditorDtoToChecklistMap : Profile
{
    public ChecklistEditorDtoToChecklistMap()
    {
        CreateMap<ChecklistEditorDto, Domain.Core.Entities.Checklist>()
            .IncludeBase<NoteBaseEditorDto, Domain.Core.Entities.NoteBase>()
            .ForMember(x => x.Options, o => o.MapFrom<CollectionResolver<ChecklistEditorDto, Domain.Core.Entities.Checklist, OptionEditorDto, Domain.Core.Entities.Option>, IEnumerable<OptionEditorDto>>(x => x.Options));
    }
}