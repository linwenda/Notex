using Microsoft.Extensions.DependencyInjection;

namespace Notex.Core.Domain.SeedWork;

public interface IEventSourcingBuilder
{
    IServiceCollection Services { get; }
}