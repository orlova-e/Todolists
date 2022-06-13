using MediatR;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Services.Commands.Notes;

public class UpdateChecklistRequest : IRequest<HandlerResult<Unit>>
{
    public Guid Id { get; }
    public ChecklistEditorDto Dto { get; }

    public UpdateChecklistRequest(Guid id, ChecklistEditorDto dto)
    {
        Id = id;
        Dto = dto;
    }
}