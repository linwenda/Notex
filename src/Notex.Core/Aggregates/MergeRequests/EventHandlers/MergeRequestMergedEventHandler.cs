using MediatR;
using Notex.Core.Aggregates.Notes.ReadModels;
using Notex.Core.Configuration;
using Notex.Messages.MergeRequests.Events;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Aggregates.MergeRequests.EventHandlers;

public class MergeRequestMergedEventHandler : IEventHandler<MergeRequestMergedEvent>
{
    private readonly IMediator _mediator;
    private readonly IReadModelRepository _readModelRepository;

    public MergeRequestMergedEventHandler(IMediator mediator,
        IReadModelRepository readModelRepository)
    {
        _mediator = mediator;
        _readModelRepository = readModelRepository;
    }

    public async Task Handle(MergeRequestMergedEvent notification, CancellationToken cancellationToken)
    {
        var sourceNoteDetail = await _readModelRepository.GetAsync<NoteDetail>(notification.SourceNoteId);

        await _mediator.Send(new DeleteNoteCommand(notification.SourceNoteId), cancellationToken);

        await _mediator.Send(new MergeNoteCommand(notification.DestinationNoteId, notification.SourceNoteId,
            sourceNoteDetail.Title, sourceNoteDetail.Content), cancellationToken);
    }
}