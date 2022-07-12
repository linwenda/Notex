using MediatR;
using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.MergeRequests.Events;
using Notex.Messages.Notes.Commands;

namespace Notex.Core.Handlers.EventHandlers;

public class MergeRequestMergedEventHandler : IEventHandler<MergeRequestMergedEvent>
{
    private readonly IMediator _mediator;
    private readonly IRepository<NoteDetail> _noteRepository;

    public MergeRequestMergedEventHandler(
        IMediator mediator,
        IRepository<NoteDetail> noteRepository)
    {
        _mediator = mediator;
        _noteRepository = noteRepository;
    }

    public async Task Handle(MergeRequestMergedEvent notification, CancellationToken cancellationToken)
    {
        var sourceNoteDetail = await _noteRepository.GetAsync(notification.SourceNoteId, cancellationToken);

        await _mediator.Send(new DeleteNoteCommand(notification.SourceNoteId), cancellationToken);

        await _mediator.Send(new MergeNoteCommand(notification.DestinationNoteId, notification.SourceNoteId,
            sourceNoteDetail.Title, sourceNoteDetail.Content), cancellationToken);
    }
}