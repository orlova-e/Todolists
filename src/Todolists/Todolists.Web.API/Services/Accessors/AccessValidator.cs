using FluentValidation;

namespace Todolists.Web.API.Services.Accessors;

public abstract class AccessValidator<TRequest> : AbstractValidator<TRequest>
{
}