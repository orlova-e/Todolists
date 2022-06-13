using FluentValidation;
using Todolists.Infrastructure.DataAccess;
using Todolists.Web.API.Services.Validation.Helpers;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Services.Validation.Checklist;

public class ChecklistEditorDtoValidator : AbstractValidator<ChecklistEditorDto>
{
    public ChecklistEditorDtoValidator(IRepository repository)
    {
        RuleFor(x => x.Id)
            .Exists<Domain.Core.Entities.Checklist, ChecklistEditorDto>(repository);
    }
}