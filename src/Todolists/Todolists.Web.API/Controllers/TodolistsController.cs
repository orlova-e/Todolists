using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolists.Web.API.Services.Commands;
using Todolists.Web.Dtos.Shared;
using Todolists.Web.API.Services.Commands.Notes;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TodolistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodolistsController(IMediator mediator)
        => _mediator = mediator;
    
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetChecklistRequest(id));
        return this.Unwrap(result);
    }
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAsync([FromQuery] GetEntitiesDto dto)
    {
        var result = await _mediator.Send(new GetChecklistsRequest(dto));
        return this.Unwrap(result);
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> PostAsync([FromBody] ChecklistCreateDto dto)
    {
        var result = await _mediator.Send(new CreateChecklistRequest(dto));
        return this.Unwrap(result);
    }
    
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] ChecklistEditorDto dto)
    {
        var result = await _mediator.Send(new UpdateChecklistRequest(id, dto));
        return this.Unwrap(result);
    }
    
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteChecklistRequest(id));
        return this.Unwrap(result);
    }
}