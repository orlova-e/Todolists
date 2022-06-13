using AutoMapper;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Translation.NoteBase;

public class NoteBaseEditorDtoToNoteBaseMap : Profile
{
    public NoteBaseEditorDtoToNoteBaseMap()
    {
        CreateMap<NoteBaseEditorDto, Domain.Core.Entities.NoteBase>()
            .ForMember(x => x.Id, o => o.Ignore())
            .ForMember(x => x.IsDeleted, o => o.Ignore())
            .ForMember(x => x.Created, o => o.Ignore())
            .ForMember(x => x.Updated, o => o.Ignore())
            .ForMember(x => x.Deleted, o => o.Ignore())
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name))
            .ForMember(x => x.UserId, o => o.Ignore())
            .ForMember(x => x.User, o => o.Ignore());
    }
}