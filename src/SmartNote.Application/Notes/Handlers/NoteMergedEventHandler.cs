using MediatR;
using SmartNote.Domain.Notes;
using SmartNote.Domain.Notes.Events;

namespace SmartNote.Application.Notes.Handlers
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
                notification.Blocks);

            await _noteRepository.SaveAsync(note);
        }
    }
}