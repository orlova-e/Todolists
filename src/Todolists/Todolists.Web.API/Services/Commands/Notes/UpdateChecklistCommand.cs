using MediatR;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Web.API.Services.Commands.Notes;

public class UpdateChecklistCommand : IRequestHandler<UpdateChecklistRequest, HandlerResult<Unit>>
{
    private readonly ILogger<UpdateChecklistCommand> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ITranslator _translator;
    private readonly IRepository _repository;

    public UpdateChecklistCommand(
        ILogger<UpdateChecklistCommand> logger,
        IDateTimeService dateTimeService,
        ITranslator translator,
        IRepository repository)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _translator = translator;
        _repository = repository;
    }

    public async Task<HandlerResult<Unit>> Handle(
        UpdateChecklistRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetAsync<Checklist, Guid>(
                Common.NotDeleted<Checklist>(request.Id), cancellationToken);
            
            entity = _translator.Transform(request.Dto, entity);
            _dateTimeService.Updated(entity);
            await _repository.UpdateAsync<Checklist, Guid>(entity, cancellationToken);
            
            return HandlerResult<Unit>.Success();
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't update a checklist:\n{message}\n{stackTrace}",
                 exc.Message, exc.StackTrace);
            
            return HandlerResult<Unit>.Exception();
        }
    }
}