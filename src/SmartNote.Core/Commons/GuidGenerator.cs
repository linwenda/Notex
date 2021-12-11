namespace SmartNote.Core.Commons
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid New() => Guid.NewGuid();
    }
}
