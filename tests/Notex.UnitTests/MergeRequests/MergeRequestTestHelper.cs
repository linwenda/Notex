using System;
using Moq;
using Notex.Core.Aggregates.MergeRequests;
using Notex.Core.Aggregates.MergeRequests.DomainServices;
using Notex.Core.Aggregates.Notes.DomainServices;
using Notex.UnitTests.Notes;

namespace Notex.UnitTests.MergeRequests;

public static class MergeRequestTestHelper
{
    public static MergeRequest CreateOpenMergeRequest()
    {
        var note = NoteTestHelper.CreateNote(new NoteOptions());

        var cloneNote = note.Clone(Guid.NewGuid(), Guid.NewGuid());

        var noteChecker = new Mock<INoteChecker>();

        noteChecker.Setup(n => n.IsPublishedNote(It.IsAny<Guid>())).Returns(true);

        var mergeRequestChecker = new Mock<IMergeRequestChecker>();

        mergeRequestChecker.Setup(n => n.HasOpenMergeRequest(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);

        return cloneNote.CreateMergeRequest(noteChecker.Object, mergeRequestChecker.Object, Guid.NewGuid(), "title",
            "description");
    }
}