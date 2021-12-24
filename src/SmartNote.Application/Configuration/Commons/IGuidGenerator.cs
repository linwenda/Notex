using SmartNote.Application.Configuration.DependencyInjection;

namespace SmartNote.Application.Configuration.Commons
{
    public interface IGuidGenerator: ITransientLifetime
    {
        Guid New();
    }
}
