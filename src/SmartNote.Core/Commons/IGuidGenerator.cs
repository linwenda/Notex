using SmartNote.Core.DependencyInjection;

namespace SmartNote.Core.Commons
{
    public interface IGuidGenerator: ITransientLifetime
    {
        Guid New();
    }
}
