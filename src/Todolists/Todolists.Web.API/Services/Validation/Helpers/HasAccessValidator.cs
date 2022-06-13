using FluentValidation;
using FluentValidation.Validators;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Web.API.Services.Validation.Helpers;

public class HasAccessValidator<TEntity, TDto> : AsyncPropertyValidator<TDto, Guid>
    where TEntity : NoteBase, new()
{
    private readonly IRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public override string Name => nameof(HasAccessValidator<TEntity, TDto>);

    public HasAccessValidator(
        IRepository repository,
        ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public override async Task<bool> IsValidAsync(
        ValidationContext<TDto> context, 
        Guid value, 
        CancellationToken cancellation)
    {
        var user = await _currentUserService.GetAsync(cancellation);
        
        var entity = await _repository.GetAsync<TEntity, Guid>(
            Common.NotDeleted<TEntity>(value), 
            cancellation);

        return user is not null &&
               entity is not null &&
               entity.UserId == user.Id;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => string.Empty;
}