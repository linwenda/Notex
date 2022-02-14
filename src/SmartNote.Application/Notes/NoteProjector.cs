using MediatR;
using SmartNote.Core.Ddd;
using SmartNote.Domain.Notes;
using SmartNote.Domain.Notes.Events;
using SmartNote.Domain.Notes.ReadModels;

namespace SmartNote.Application.Notes
{
    public class NoteProjector :
        INotificationHandler<NoteCreatedEvent>,
        INotificationHandler<NotePublishedEvent>,
        INotificationHandler<NoteDeletedEvent>,
        INotificationHandler<NoteForkedEvent>,
        INotificationHandler<NoteMergedEvent>,
        INotificationHandler<NoteUpdatedEvent>
    {
        private readonly IRepository<NoteReadModel> _noteRepository;
        private readonly IRepository<NoteHistoryReadModel> _noteHistoryRepository;

        public NoteProjector(
            IRepository<NoteReadModel> noteRepository,
            IRepository<NoteHistoryReadModel> noteHistoryRepository)
        {
            _noteRepository = noteRepository;
            _noteHistoryRepository = noteHistoryRepository;
        }

        public async Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
        {
            var node = new NoteReadModel
            {
                Id = notification.NoteId,
                CreationTime = notification.CreationTime,
                AuthorId = notification.AuthorId,
                SpaceId = notification.SpaceId,
                Title = notification.Title,
                Version = 1,
                IsDeleted = false,
                Status = NoteStatus.Draft
            };

            await _noteRepository.InsertAsync(node);
        }

        public async Task Handle(NoteUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetAsync(n => n.Id == notification.NoteId);

            note.Content = notification.Content;

            if (note.Status == NoteStatus.Published)
            {
                note.Version++;
            }

            await _noteRepository.UpdateAsync(note);
            await InsertNoteHistoryAsync(note);
        }

        public async Task Handle(NotePublishedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetAsync(n => n.Id == notification.NoteId);

            note.Status = NoteStatus.Published;

            await _noteRepository.UpdateAsync(note);
            await InsertNoteHistoryAsync(note);
        }

        public async Task Handle(NoteForkedEvent notification, CancellationToken cancellationToken)
        {
            var node = new NoteReadModel
            {
                Id = notification.NoteId,
                ForkId = notification.FromNoteId,
                AuthorId = notification.AuthorId,
                CreationTime = notification.CreationTime,
                Title = notification.Title,
                Version = 1,
                IsDeleted = false,
                Status = NoteStatus.Draft
            };

            await _noteRepository.InsertAsync(node);
        }


        public async Task Handle(NoteDeletedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetAsync(n => n.Id == notification.NoteId);

            note.IsDeleted = true;

            await _noteRepository.UpdateAsync(note);
        }

        public async Task Handle(NoteMergedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetAsync(n => n.Id == notification.NoteId);

            note.Title = notification.Title;
            note.Version += 1;

            await _noteRepository.UpdateAsync(note);
            await InsertNoteHistoryAsync(note, $"Merged by note id:{notification.FromNoteId}");
        }

        private async Task InsertNoteHistoryAsync(NoteReadModel note, string comment = "")
        {
            if (note.Status == NoteStatus.Published)
            {
                await _noteHistoryRepository.InsertAsync(new NoteHistoryReadModel
                {
                    NoteId = note.Id,
                    AuthorId = note.AuthorId,
                    CreationTime = DateTime.UtcNow,
                    Title = note.Title,
                    Blocks = note.Content,
                    Version = note.Version,
                    Comment = comment
                });
            }
        }
    }
}