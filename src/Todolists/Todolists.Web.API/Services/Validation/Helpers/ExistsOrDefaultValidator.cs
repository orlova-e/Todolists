using FluentValidation;
using FluentValidation.Validators;
using Todolists.Domain.Core.Interfaces;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;

namespace Todolists.Web.API.Services.Validation.Helpers;

public class ExistsOrDefaultValidator<TEntity, TDto> : AsyncPropertyValidator<TDto, Guid?>
    where TEntity : class, IDomainEntity
{
    private readonly IRepository _repository;
    
    public override string Name => nameof(ExistsValidator<TEntity, TDto>);

    public ExistsOrDefaultValidator(IRepository repository)
        => _repository = repository;

    public override async Task<bool> IsValidAsync(
        ValidationContext<TDto> context, 
        Guid? value, 
        CancellationToken cancellation)
    {
        if (value is null)
            return true;

        var count = await _repository.CountAsync<TEntity, Guid>(
            Common.NotDeleted<TEntity>((Guid) value),
            cancellation);
        
        return count is 1;
    }
    
    protected override string GetDefaultMessageTemplate(string errorCode)
        => $"Entity of type '{typeof(TEntity).Name}' is not null or does not exist";
}