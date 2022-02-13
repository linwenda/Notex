namespace SmartNote.Core.Domain.Notes
{
    public class NoteId : IEventSourcedAggregateKey
    {
        public NoteId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }
    }
}