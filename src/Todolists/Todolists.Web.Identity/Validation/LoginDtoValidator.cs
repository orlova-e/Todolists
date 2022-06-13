using FluentValidation;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.Identity.Dto;

namespace Todolists.Web.Identity.Validation
{
    internal class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        private readonly IRepository _repository;
        private readonly IEncryptionService _encryptionService;

        public LoginDtoValidator(
            IRepository repository,
            IEncryptionService encryptionService)
        {
            _repository = repository;
            _encryptionService = encryptionService;

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(ValidationMessages.EmailRequired)
                .MustAsync(DoesAccountExists)
                .WithMessage(ValidationMessages.AccountDoesNotExist);

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(ValidationMessages.PasswordRequired)
                .MinimumLength(8)
                .WithMessage(ValidationMessages.PasswordMinLength)
                .MustAsync(IsPasswordValid)
                .WithMessage(ValidationMessages.PasswordInvalid);
        }

        private async Task<bool> DoesAccountExists(
            LoginDto dto,
            string email,
            CancellationToken cancellationToken)
        {
            var count = await _repository.CountAsync<Account, Guid>(
                Accounts.ByEmailOrUsername(email) &
                Common.NotDeleted<Account>(), cancellationToken);
            
            return count is 1;
        }

        private async Task<bool> IsPasswordValid(
            LoginDto dto, 
            string password,
            CancellationToken cancellationToken)
        {
            var account = await _repository.GetAsync<Account, Guid>(
                Accounts.ByEmail(dto.Name) &
                Common.NotDeleted<Account>(), cancellationToken);
            
            return account is not null &&
                   _encryptionService.Validate(account.Hash, dto.Password);
        }
    }
}
