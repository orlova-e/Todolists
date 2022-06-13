using MediatR;
using Todolists.Domain.Core.Entities;
using Todolists.Web.Dtos.Checklist;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.API.Services.Commands.Notes;

public class CreateChecklistRequest : IRequest<HandlerResult<Guid>>
{
    public ChecklistCreateDto Dto { get; }

    public CreateChecklistRequest(ChecklistCreateDto dto)
    {
        Dto = dto;
    }
}