﻿namespace SmartNote.Core.Application.NoteCooperations.Contracts
{
    public class GetNoteCooperationsQuery : IQuery<IEnumerable<NoteCooperationDto>>
    {
        public Guid NoteId { get; }

        public GetNoteCooperationsQuery(Guid noteId)
        {
            NoteId = noteId;
        }
    }
}