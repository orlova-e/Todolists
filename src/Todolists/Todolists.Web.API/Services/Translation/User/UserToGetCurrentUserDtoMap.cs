using AutoMapper;
using Todolists.Web.Dtos.Account;

namespace Todolists.Web.API.Services.Translation.User;

public class UserToGetCurrentUserDtoMap : Profile
{
    public UserToGetCurrentUserDtoMap()
    {
        CreateMap<Domain.Core.Entities.User, GetCurrentUserDto>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));
    }
}