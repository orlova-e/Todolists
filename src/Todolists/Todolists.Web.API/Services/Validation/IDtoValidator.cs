using FluentValidation;

namespace Todolists.Web.API.Services.Validation;

public interface IDtoValidator<in TRequest>: IValidator<TRequest>
{
}