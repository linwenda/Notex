using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.NoteMergeRequests.Commands
{
    public class CreateNoteMergeRequestCommand : ICommand<Unit>
    {
        public Guid NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}