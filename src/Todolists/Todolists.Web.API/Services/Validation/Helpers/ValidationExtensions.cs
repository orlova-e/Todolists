using FluentValidation;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Core.Interfaces;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Web.API.Services.Validation.Helpers;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<TEditorDto, Guid> Exists<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetAsyncValidator(new ExistsValidator<TEntity, TEditorDto>(repository));
    }
    
    public static IRuleBuilderOptions<TEditorDto, Guid?> ExistsOrDefault<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid?> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetAsyncValidator(new ExistsOrDefaultValidator<TEntity, TEditorDto>(repository));
    }
    
    public static IRuleBuilderOptions<TEditorDto, Guid> ExistsOrEmpty<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetAsyncValidator(new ExistsOrEmptyValidator<TEntity, TEditorDto>(repository));
    }
    
    public static IRuleBuilderOptions<TEditorDto, Guid> NotExists<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetAsyncValidator(new NotExistsValidator<TEntity, TEditorDto>(repository));
    }
    
    public static IRuleBuilderOptions<TEditorDto, Guid> Belongs<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid> ruleBuilder, 
        IRepository repository,
        ICurrentUserService currentUserService)
        where TEntity : NoteBase, new()
    {
        return ruleBuilder.SetAsyncValidator(new HasAccessValidator<TEntity, TEditorDto>(repository, currentUserService));
    }
}