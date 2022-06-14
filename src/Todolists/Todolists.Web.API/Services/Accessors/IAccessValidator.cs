using FluentValidation;

namespace Todolists.Web.API.Services.Accessors;

public interface IAccessValidator <in TRequest>: IValidator<TRequest>
{
}