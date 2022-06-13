using MediatR;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Web.API.Services.Commands.Notes;

public class DeleteChecklistCommand : IRequestHandler<DeleteChecklistRequest, HandlerResult<Unit>>
{
    private readonly ILogger<DeleteChecklistCommand> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepository _repository;

    public DeleteChecklistCommand(
        ILogger<DeleteChecklistCommand> logger,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService,
        IRepository repository)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
        _repository = repository;
    }

    public async Task<HandlerResult<Unit>> Handle(
        DeleteChecklistRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetAsync<Checklist, Guid>(
                Common.NotDeleted<Checklist>(request.Id), cancellationToken);
            
            _dateTimeService.Deleted(entity);
            entity.IsDeleted = true;
            await _repository.UpdateAsync<Checklist, Guid>(entity, cancellationToken);
            
            return HandlerResult<Unit>.Success();
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't delete a checklist:\n{message}\n{stackTrace}",
                 exc.Message, exc.StackTrace);
            
            return HandlerResult<Unit>.Exception();
        }
    }
}