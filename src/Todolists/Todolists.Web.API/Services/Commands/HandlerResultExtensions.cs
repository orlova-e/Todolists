using Microsoft.AspNetCore.Mvc;

namespace Todolists.Web.API.Services.Commands;

public static class HandlerResultExtensions
{
    public static IActionResult Unwrap<T>(
        this ControllerBase controller,
        HandlerResult<T> result)
    {
        return result.Status switch
        {
            OperationStatus.Forbidden => controller.Forbid(),
            OperationStatus.ValidationFailure => controller.BadRequest(result.ValidationFailures),
            OperationStatus.Success => controller.Ok(result.Result),
            OperationStatus.InternalError => controller.Problem(result.FailureMessage),
            { } => controller.Problem()
        };
    }
}