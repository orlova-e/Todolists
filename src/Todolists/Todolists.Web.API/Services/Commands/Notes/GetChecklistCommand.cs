using MediatR;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Services.Commands.Notes;

public class GetChecklistCommand : IRequestHandler<GetChecklistRequest, HandlerResult<ChecklistGetDto>>
{
    private readonly ILogger<GetChecklistCommand> _logger;
    private readonly ITranslator _translator;
    private readonly IRepository _repository;

    public GetChecklistCommand(
        ILogger<GetChecklistCommand> logger,
        ITranslator translator,
        IRepository repository)
    {
        _logger = logger;
        _translator = translator;
        _repository = repository;
    }

    public async Task<HandlerResult<ChecklistGetDto>> Handle(
        GetChecklistRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetAsync<Checklist, Guid>(
                Common.NotDeleted<Checklist>(request.Id), cancellationToken);

            var dto = _translator.Translate<Checklist, ChecklistGetDto>(entity);
            return HandlerResult<ChecklistGetDto>.Success(dto);
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't get a checklist with id '{id}':\n{message}\n{stackTrace}",
                 request.Id, exc.Message, exc.StackTrace);
            
            return HandlerResult<ChecklistGetDto>.Exception();
        }
    }
}