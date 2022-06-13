using AutoMapper;
using Todolists.Web.Dtos.Checklist;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Translation.Checklist;

public class ChecklistCreateDtoToChecklistMap : Profile
{
    public ChecklistCreateDtoToChecklistMap()
    {
        CreateMap<ChecklistCreateDto, Domain.Core.Entities.Checklist>()
            .IncludeBase<NoteBaseCreateDto, Domain.Core.Entities.NoteBase>()
            .ForMember(x => x.Options, o => o.MapFrom(x => x.Options));
    }
}