using System;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;

namespace MarchNote.UnitTests.Notes
{
    public class NoteTestUtil
    {
        internal static NoteData CreatePublishedNote()
        {
            var space = Space.Create(new UserId(Guid.NewGuid()), "space", "#FFF", "Bear");
            var note = new Note(new NoteId(Guid.NewGuid()));
            note.Create(space, space.AuthorId, "title", "content");
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