using FluentValidation;
using Todolists.Infrastructure.DataAccess;
using Todolists.Web.API.Services.Commands.Notes;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Services.Validation.Checklist;

public class UpdateChecklistRequestValidator : DataValidator<UpdateChecklistRequest>
{
    public UpdateChecklistRequestValidator(IRepository repository)
    {
        RuleFor(x => x.Dto)
            .SetValidator(new ChecklistEditorDtoValidator(repository))
            .OverridePropertyName(string.Empty);
    }
}