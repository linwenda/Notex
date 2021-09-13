using System;
using System.Collections.Generic;
using MarchNote.Domain.Notes;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.UnitTests.Notes
{
    public static class NoteTestUtil
    {
        internal static NoteData CreatePublishedNote()
        {
            var space = Space.Create(new UserId(Guid.NewGuid()), "space", "#FFF", "Bear");
            var note = Note.Create(space, space.AuthorId, "title", "content", new List<string>());
            note.Publish(space.AuthorId);

            return new NoteData(note, space.AuthorId);
        }

        internal class NoteData
        {
            public NoteData(Note note, UserId authorId)
            {
                Note = note;
                AuthorId = authorId;
            }

            public Note Note { get; }
            public UserId AuthorId { get; }
        }
    }
}