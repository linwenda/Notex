using Notex.Core.Domain.Notes.ReadModels;
using Notex.Core.Domain.SeedWork;
using Notex.Messages.Notes;
using Notex.Messages.Notes.Events;

namespace Notex.Core.Handlers.ReadModelGenerators;

public class NoteReadModelGenerator :
    IEventHandler<NoteCreatedEvent>,
    IEventHandler<NoteEditedEvent>,
    IEventHandler<NoteDeletedEvent>,
    IEventHandler<NotePublishedEvent>,
    IEventHandler<NoteRestoredEvent>,
    IEventHandler<NoteMergedEvent>,
    IEventHandler<NoteVisibilityChangedEvent>,
    IEventHandler<NoteTagsUpdatedEvent>
{
    private readonly IRepository<NoteDetail> _noteRepository;
    private readonly IRepository<NoteHistory> _noteHistoryRepository;

    public NoteReadModelGenerator(IRepository<NoteDetail> noteRepository, IRepository<NoteHistory> noteHistoryRepository)
    {
        _noteRepository = noteRepository;
        _noteHistoryRepository = noteHistoryRepository;
    }

    public async Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
    {
        var note = new NoteDetail
        {
            Id = notification.SourcedId,
            Content = notification.Content,
            Status = notification.Status,
            Title = notification.Title,
            Visibility = notification.Visibility,
            CreatorId = notification.CreatorId,
            CreationTime = DateTime.UtcNow,
            SpaceId = notification.SpaceId,
            CloneFormId = notification.CloneFormId,
            Version = notification.Status == NoteStatus.Draft ? 0 : 1
        };

        await _noteRepository.InsertAsync(note, cancellationToken);

        await InsertHistoryAsync(note, DateTime.Now, cancellationToken: cancellationToken);
    }

    public async Task Handle(NoteEditedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(notification.SourcedId, cancellationToken);

        note.Title = notification.Title;
        note.Content = notification.Content;
        note.LastModifierId = notification.UserId;
        note.LastModificationTime = DateTime.UtcNow;

        if (notification.Status == NoteStatus.Published)
        {
            note.Version += 1;
        }

        await _noteRepository.UpdateAsync(note, cancellationToken);

        await InsertHistoryAsync(note, DateTime.Now, cancellationToken: cancellationToken);
    }

    public async Task Handle(NoteDeletedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(notification.SourcedId, cancellationToken);

        note.IsDeleted = true;

        await _noteRepository.UpdateAsync(note, cancellationToken);
    }

    public async Task Handle(NotePublishedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(notification.SourcedId, cancellationToken);

        note.Status = NoteStatus.Published;
        note.Version += 1;

        await _noteRepository.UpdateAsync(note, cancellationToken);

        await InsertHistoryAsync(note, DateTime.Now, cancellationToken: cancellationToken);
    }

    public async Task Handle(NoteRestoredEvent notification, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(notification.SourcedId, cancellationToken);

        note.Title = notification.Title;
        note.Content = notification.Content;
        note.Version += 1;

        await _noteRepository.UpdateAsync(note, cancellationToken);

        await InsertHistoryAsync(note, DateTime.Now, $"Restored from v{notification.HistoryVersion}",
            cancellationToken);
    }

    public async Task Handle(NoteMergedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(notification.SourcedId, cancellationToken);

        note.Title = notification.Title;
        note.Content = notification.Content;
        note.Version += 1;

        await _noteRepository.UpdateAsync(note, cancellationToken);

        await InsertHistoryAsync(note, DateTime.Now, $"Merge from {notification.SourceNoteId}",
            cancellationToken);
    }

    public async Task Handle(NoteVisibilityChangedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(notification.SourcedId, cancellationToken);

        note.Visibility = notification.Visibility;

        await _noteRepository.UpdateAsync(note, cancellationToken);
    }

    public async Task Handle(NoteTagsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetAsync(notification.SourcedId, cancellationToken);

        note.Tags = notification.Tags.ToArray();

        await _noteRepository.UpdateAsync(note, cancellationToken);
    }

    private async Task InsertHistoryAsync(NoteDetail note, DateTime creationTime, string comment = "",
        CancellationToken cancellationToken = default)
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

        await _noteHistoryRepository.InsertAsync(noteHistory, cancellationToken);
    }
}