using FluentValidation;

namespace Todolists.Web.API.Services.Validation;

public abstract class DataValidator<TDto> : AbstractValidator<TDto>
{
}