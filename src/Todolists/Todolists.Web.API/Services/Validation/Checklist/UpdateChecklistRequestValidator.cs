using FluentValidation;
using Todolists.Web.API.Services.Commands.Notes;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Services.Validation.Checklist;

public class UpdateChecklistRequestValidator : DataValidator<UpdateChecklistRequest>
{
    public UpdateChecklistRequestValidator(IValidator<ChecklistEditorDto> validator)
    {
        RuleFor(x => x.Dto)
            .SetValidator(validator).When(_ => validator is not null)
            .OverridePropertyName(string.Empty);
    }
}