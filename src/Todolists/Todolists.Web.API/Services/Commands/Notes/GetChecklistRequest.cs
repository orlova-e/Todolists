using MediatR;
using Todolists.Domain.Core.Entities;
using Todolists.Web.Dtos.Checklist;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Commands.Notes;

public class GetChecklistRequest : IRequest<HandlerResult<ChecklistGetDto>>
{
    public Guid Id { get; }

    public GetChecklistRequest(Guid id)
    {
        Id = id;
    }
}