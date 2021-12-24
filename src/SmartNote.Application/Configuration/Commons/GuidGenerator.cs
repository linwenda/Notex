namespace SmartNote.Application.Configuration.Commons
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid New() => Guid.NewGuid();
    }
}
