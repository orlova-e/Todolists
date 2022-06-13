using FluentValidation;
using Todolists.Domain.Core.Entities;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.API.Services.Validation.Helpers;
using Todolists.Web.API.Services.Commands.Notes;

namespace Todolists.Web.API.Services.Accessors.Shared;

public class DeleteNoteRequestAccessor : AccessValidator<DeleteChecklistRequest>
{
    public DeleteNoteRequestAccessor(
        IRepository repository,
        ICurrentUserService currentUserService)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .Exists<Checklist, DeleteChecklistRequest>(repository)
            .Belongs<Checklist, DeleteChecklistRequest>(repository, currentUserService);
    }
}