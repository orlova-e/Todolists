using FluentValidation;
using Todolists.Domain.Core.Entities;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.API.Services.Validation.Helpers;
using Todolists.Web.API.Services.Commands.Notes;

namespace Todolists.Web.API.Services.Accessors.Shared;

public class UpdateNoteRequestAccessor : AccessValidator<UpdateChecklistRequest>
{
    public UpdateNoteRequestAccessor(
        IRepository repository,
        ICurrentUserService currentUserService)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .Exists<Checklist, UpdateChecklistRequest>(repository)
            .Belongs<Checklist, UpdateChecklistRequest>(repository, currentUserService);

        When(x => x.Dto is not null, () =>
        {
            RuleFor(x => x.Dto.Id)
                .NotEmpty()
                .OverridePropertyName(string.Empty);
            
            RuleFor(x => x)
                .Must(IdsEqual);
        });
    }

    private bool IdsEqual(UpdateChecklistRequest request)
        => request.Id == request.Dto?.Id;
}