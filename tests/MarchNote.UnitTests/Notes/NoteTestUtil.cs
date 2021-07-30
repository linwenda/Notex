using System;
using MarchNote.Domain.NoteAggregate;
using MarchNote.Domain.Users;

namespace MarchNote.UnitTests.Notes
{
    public class NoteTestUtil
    {
        internal static NoteData CreatePublishedNote()
        {
            var note = new Note(new NoteId(Guid.NewGuid()));
            var authorId = new UserId(Guid.NewGuid());
            note.Create(authorId, "title", "content");
            note.Publish(authorId);
            
            return new NoteData(note, authorId);
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