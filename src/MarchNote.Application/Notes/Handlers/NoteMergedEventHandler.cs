using System.Threading;
using System.Threading.Tasks;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Notes.Events;
using MarchNote.Domain.Users;
using MediatR;

namespace MarchNote.Application.Notes.Handlers
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
                new UserId(notification.AuthorId), 
                notification.Title, 
                notification.Content, 
                notification.Tags);

            await _noteRepository.SaveAsync(note);
        }
    }
}