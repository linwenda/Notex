using System;
using Moq;
using Notex.Core.Aggregates.Notes;
using Notex.Core.Aggregates.Spaces;
using Notex.Core.Aggregates.Spaces.DomainServices;
using Notex.Messages.Notes;
using Notex.Messages.Shared;

namespace Notex.UnitTests.Notes;

public static class NoteTestHelper
{
    public static Note CreateNote(NoteOptions options)
    {
        var authorId = options.UserId ?? new FakeCurrentUser().Id;

        var mockSpaceChecker = new Mock<ISpaceChecker>();
        mockSpaceChecker.Setup(s => s.IsUniqueNameInUserSpace(It.IsAny<Guid>(), It.IsAny<string>()))
            .Returns(true);

        var space = Space.Initialize(mockSpaceChecker.Object, authorId, "microsoft", "https://image.microsoft.com",
            Visibility.Public);

        return space.CreateNote(options.Title ?? "microsoft", options.Content ?? ".Net 6 new feature", options.Status);
    }
}

public class NoteOptions
{
    public Guid? UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteStatus Status { get; set; } = NoteStatus.Published;
}