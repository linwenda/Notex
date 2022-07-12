using Notex.Core.Domain.SeedWork;
using Notex.IntegrationTests.EventSourcing.Domain;
using Xunit;

namespace Notex.IntegrationTests.EventSourcing;

[Collection("Sequence")]
public class DependencyInjectionTests : IClassFixture<StartupFixture>
{
    private readonly StartupFixture _fixture;

    public DependencyInjectionTests(StartupFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void CanGetServices()
    {
        var eventStore = _fixture.GetService<IEventStore>();
        Assert.NotNull(eventStore);

        var mementoStore = _fixture.GetService<IMementoStore>();
        Assert.NotNull(mementoStore);

        var eventSourcedRepository = _fixture.GetService<IEventSourcedRepository<Post>>();
        Assert.NotNull(eventSourcedRepository);
    }
}