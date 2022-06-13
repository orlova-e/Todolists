using MediatR;
using Todolists.Web.Dtos.Account;

namespace Todolists.Web.API.Services.Commands.User;

public class CreateUserAccountRequest : IRequest<HandlerResult<Guid>>
{
    public CreateUserAccountDto Dto { get; }

    public CreateUserAccountRequest(CreateUserAccountDto dto)
    {
        Dto = dto;
    }
}