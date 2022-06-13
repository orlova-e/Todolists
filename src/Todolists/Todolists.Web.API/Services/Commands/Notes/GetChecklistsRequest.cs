using MediatR;
using Todolists.Web.Dtos.Checklist;
using Todolists.Web.Dtos.Shared;

namespace Todolists.Web.API.Services.Commands.Notes;

public class GetChecklistsRequest : IRequest<HandlerResult<ListDto<ChecklistGetDto>>>
{
    public GetEntitiesDto Dto { get; }

    public GetChecklistsRequest(GetEntitiesDto dto)
    {
        Dto = dto;
    }
}