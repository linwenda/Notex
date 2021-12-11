using SmartNote.Core.DependencyInjection;

namespace SmartNote.Core.Commons
{
    public interface IClock : ITransientLifetime
    {
        DateTimeOffset Now { get; }
    }
}
