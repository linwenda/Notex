using MediatR;
using SmartNote.Core.Domain.Notes;
using SmartNote.Core.Domain.Notes.Events;

namespace SmartNote.Core.Application.Notes.Handlers
{
    public class NoteMergedEventHandler : INotificationHandler<NoteMergedEvent>
    {
        private readonly INoteRepository _noteRepository;

        public NoteMergedEventHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task Handle(NoteMergedEvent notification, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.LoadAsync(new NoteId(notification.FromNoteId));

            note.Update(
                notification.AuthorId,
                notification.Title,
                notification.Content,
                notification.Tags);

            await _noteRepository.SaveAsync(note);
        }
    }
}