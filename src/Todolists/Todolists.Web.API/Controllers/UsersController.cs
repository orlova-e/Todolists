using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolists.Web.Dtos.Account;
using Todolists.Web.API.Services.Commands;
using Todolists.Web.API.Services.Commands.User;

namespace Todolists.Web.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromForm] CreateUserAccountDto dto)
    {
        var result = await _mediator.Send(new CreateUserAccountRequest(dto));
        return this.Unwrap(result);
    }
}