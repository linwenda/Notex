using SmartNote.Core.Lifetime;

namespace SmartNote.Core.Clocks;

public interface IClock : ITransientLifetime
{
    DateTime Now { get; }
}