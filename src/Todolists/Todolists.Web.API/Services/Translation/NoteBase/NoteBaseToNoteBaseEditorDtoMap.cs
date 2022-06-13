using AutoMapper;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Translation.NoteBase;

public class NoteBaseToNoteBaseEditorDtoMap : Profile
{
    public NoteBaseToNoteBaseEditorDtoMap()
    {
        CreateMap<Domain.Core.Entities.NoteBase, NoteBaseEditorDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));
    }
}