using SmartNote.Core.Lifetime;

namespace SmartNote.Core.Guids;

public interface IGuidGenerator : ITransientLifetime
{
    Guid New();
}