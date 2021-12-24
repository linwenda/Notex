using SmartNote.Application.Configuration.DependencyInjection;

namespace SmartNote.Application.Configuration.Commons
{
    public interface IClock : ITransientLifetime
    {
        DateTimeOffset Now { get; }
    }
}
