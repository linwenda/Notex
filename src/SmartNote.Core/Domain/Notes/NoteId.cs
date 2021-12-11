namespace SmartNote.Core.Domain.Notes
{
    public class NoteId : IAggregateIdentity
    {
        public NoteId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }
    }
}