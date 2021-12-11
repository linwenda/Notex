using MediatR;

namespace SmartNote.Core.Application.NoteMergeRequests.Contracts
{
    public class CreateNoteMergeRequestCommand : ICommand<Unit>
    {
        public Guid NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}