using FluentValidation;
using FluentValidation.Validators;
using Todolists.Domain.Core.Interfaces;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;

namespace Todolists.Web.API.Services.Validation.Helpers;

public class ExistsValidator<TEntity, TDto> : AsyncPropertyValidator<TDto, Guid>
    where TEntity : class, IDomainEntity
{
    private readonly IRepository _repository;
    
    public override string Name => nameof(ExistsValidator<TEntity, TDto>);

    public ExistsValidator(IRepository repository)
        => _repository = repository;

    public override async Task<bool> IsValidAsync(
        ValidationContext<TDto> context, 
        Guid value, 
        CancellationToken cancellation)
    {
        var count = await _repository.CountAsync<TEntity, Guid>(
            Common.NotDeleted<TEntity>(value), 
            cancellation);
        
        return count is 1;
    }
    
    protected override string GetDefaultMessageTemplate(string errorCode)
        => $"Entity of type '{typeof(TEntity).Name}' does not exist";
}