using FluentValidation;

namespace Todolists.Web.API.Services.Validation;

public abstract class DtoValidator<TDto> : AbstractValidator<TDto>, IDtoValidator<TDto>
{
}