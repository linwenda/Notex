using System;

namespace Notex.Messages.MergeRequests.Commands;

public class CreateMergeRequestCommand : ICommand<Guid>
{
    public Guid NoteId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}