using AutoMapper;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Translation.Note;

public class NoteBaseToNoteBaseGetDtoMap : Profile
{
    public NoteBaseToNoteBaseGetDtoMap()
    {
        CreateMap<Domain.Core.Entities.NoteBase, NoteBaseGetDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Created, o => o.MapFrom(x => x.Created))
            .ForMember(x => x.Updated, o => o.MapFrom(x => x.Updated))
            .ForMember(x => x.Deleted, o => o.MapFrom(x => x.Deleted))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));
    }
}