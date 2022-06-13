using MediatR;
using Todolists.Domain.Core.Entities;

namespace Todolists.Web.API.Services.Commands.Notes;

public class DeleteChecklistRequest : IRequest<HandlerResult<Unit>>
{
    public Guid Id { get; }

    public DeleteChecklistRequest(Guid id)
    {
        Id = id;
    }
}