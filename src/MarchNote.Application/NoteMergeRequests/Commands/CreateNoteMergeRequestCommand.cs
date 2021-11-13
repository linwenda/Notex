using System;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.NoteMergeRequests.Commands
{
    public class CreateNoteMergeRequestCommand : ICommand<Unit>
    {
        public Guid NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}