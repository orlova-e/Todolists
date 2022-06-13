using FluentValidation;
using Todolists.Domain.Core.Conditions;
using Todolists.Web.Dtos.Shared;

namespace Todolists.Web.API.Services.Validation.Shared;

public class GetEntitiesDtoValidator : DataValidator<GetEntitiesDto>
{
    public GetEntitiesDtoValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.ItemsNumber)
            .GreaterThan(0);

        RuleFor(x => x.SortDir)
            .Must(IsSortDirDefined);
    }

    private bool IsSortDirDefined(SortDir sortDir)
        => Enum.IsDefined(sortDir);
}