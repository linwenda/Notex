﻿using System;
using System.Collections.Generic;
using SmartNote.Core.Domain.Notes;
using SmartNote.UnitTests.Spaces;

namespace SmartNote.UnitTests.Notes
{
    public static class NoteTestUtil
    {
        internal static NoteData CreatePublishedNote()
        {
            var space = SpaceTestUtil.CreateSpace();
            
            var note = space.CreateNote(space.AuthorId, "title", "content", new List<string>());
            note.Publish(space.AuthorId);

            return new NoteData(note, space.AuthorId);
        }

        internal class NoteData
        {
            public NoteData(Note note, Guid authorId)
            {
                Note = note;
                AuthorId = authorId;
            }

            public Note Note { get; }
            public Guid AuthorId { get; }
        }
    }
}