using Notex.Core.Configuration;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Events;

namespace Notex.Core.Aggregates.Notes.ReadModels;

public class NoteDetailProjection :
    IEventHandler<NoteCreatedEvent>,
    IEventHandler<NoteEditedEvent>,
    IEventHandler<NoteDeletedEvent>,
    IEventHandler<NotePublishedEvent>,
    IEventHandler<NoteRestoredEvent>,
    IEventHandler<NoteMergedEvent>,
    IEventHandler<NoteVisibilityChangedEvent>,
    IEventHandler<NoteTagsUpdatedEvent>
{
    private readonly IReadModelRepository _readModelRepository;

    public NoteDetailProjection(IReadModelRepository readModelRepository)
    {
        _readModelRepository = readModelRepository;
    }

    public async Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
    {
        var note = new NoteDetail();

        note.When(notification);

        await _readModelRepository.InsertAsync(note);

        await InsertHistoryAsync(note, notification.OccurrenceTime);
    }

    public async Task Handle(NoteEditedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _readModelRepository.GetAsync<NoteDetail>(notification.AggregateId);

        note.When(notification);

        await _readModelRepository.UpdateAsync(note);

        await InsertHistoryAsync(note, notification.OccurrenceTime);
    }

    public async Task Handle(NoteDeletedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _readModelRepository.GetAsync<NoteDetail>(notification.AggregateId);

        note.When(notification);

        await _readModelRepository.UpdateAsync(note);
    }

    public async Task Handle(NotePublishedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _readModelRepository.GetAsync<NoteDetail>(notification.AggregateId);

        note.When(notification);

        await _readModelRepository.UpdateAsync(note);

        await InsertHistoryAsync(note, notification.OccurrenceTime);
    }

    public async Task Handle(NoteRestoredEvent notification, CancellationToken cancellationToken)
    {
        var note = await _readModelRepository.GetAsync<NoteDetail>(notification.AggregateId);

        note.When(notification);

        await _readModelRepository.UpdateAsync(note);

        var noteHistory = new NoteHistory
        {
            Id = Guid.NewGuid(),
            NoteId = note.Id,
            Title = note.Title,
            Content = note.Content,
            Version = note.Version,
            CreationTime = notification.OccurrenceTime,
            CloneFormId = note.CloneFormId,
            Comment = $"Restored from v{notification.HistoryVersion}"
        };

        await _readModelRepository.InsertAsync(noteHistory);
    }

    public async Task Handle(NoteMergedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _readModelRepository.GetAsync<NoteDetail>(notification.AggregateId);

        note.When(notification);

        await _readModelRepository.UpdateAsync(note);

        var noteHistory = new NoteHistory
        {
            Id = Guid.NewGuid(),
            NoteId = note.Id,
            Title = note.Title,
            Content = note.Content,
            Version = note.Version,
            CreationTime = notification.OccurrenceTime,
            CloneFormId = note.CloneFormId,
            Comment = $"Merge from {notification.SourceNoteId}"
        };

        await _readModelRepository.InsertAsync(noteHistory);
    }

    public async Task Handle(NoteVisibilityChangedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _readModelRepository.GetAsync<NoteDetail>(notification.AggregateId);

        note.When(notification);

        await _readModelRepository.UpdateAsync(note);
    }

    public async Task Handle(NoteTagsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _readModelRepository.GetAsync<NoteDetail>(notification.AggregateId);

        note.When(notification);

        await _readModelRepository.UpdateAsync(note);
    }

    private async Task InsertHistoryAsync(NoteDetail note, DateTimeOffset creationTime, string comment = "")
    {
        if (note.Status == NoteStatus.Draft) return;

        var noteHistory = new NoteHistory
        {
            Id = Guid.NewGuid(),
            NoteId = note.Id,
            Title = note.Title,
            Content = note.Content,
            Version = note.Version,
            CreationTime = creationTime,
            CloneFormId = note.CloneFormId,
            Comment = comment
        };

        await _readModelRepository.InsertAsync(noteHistory);
    }
}