using FluentValidation;
using Todolists.Infrastructure.DataAccess;
using Todolists.Web.API.Services.Commands.User;

namespace Todolists.Web.API.Services.Validation.User;

public class CreateUserAccountRequestValidator : DataValidator<CreateUserAccountRequest>
{
    public CreateUserAccountRequestValidator(IRepository repository)
    {
        RuleFor(x => x.Dto)
            .SetValidator(new CreateUserAccountDtoValidator(repository))
            .OverridePropertyName(string.Empty);
    }
}