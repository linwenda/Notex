namespace SmartNote.Core.Guids;

public class GuidGenerator : IGuidGenerator
{
    public Guid New()
    {
        return Guid.NewGuid();
    }
}