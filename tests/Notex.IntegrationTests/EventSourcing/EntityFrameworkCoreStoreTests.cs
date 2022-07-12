using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Notex.Core.Domain.SeedWork;
using Notex.IntegrationTests.EventSourcing.Domain;
using Notex.Messages;
using Xunit;

namespace Notex.IntegrationTests.EventSourcing;

[Collection("Sequence")]
public class EntityFrameworkCoreStoreTests : IClassFixture<StartupFixture>
{
    private readonly IEventStore _eventStore;
    private readonly IMementoStore _mementoStore;

    public EntityFrameworkCoreStoreTests(StartupFixture fixture)
    {
        _eventStore = fixture.GetService<IEventStore>();
        _mementoStore = fixture.GetService<IMementoStore>();
    }
    
    [Fact]
    public async Task CanSaveEvents()
    {
        var sourcedId = Guid.NewGuid();

        var expectedEvents = new List<IVersionedEvent>
        {
            new PostCreatedEvent(sourcedId, 1, "test", "test", Guid.NewGuid()),
            new PostEditedEvent(sourcedId, 2, "test", "test")
        };

        await _eventStore.SaveAsync(expectedEvents);

        var actualEvents = await _eventStore.LoadAsync(sourcedId, 0);

        Assert.Equal(2, actualEvents.Count());

        foreach (var expectedEvent in expectedEvents)
        {
            var actualEvent = actualEvents.Single(e =>
                e.SourcedId == expectedEvent.SourcedId && e.Version == expectedEvent.Version);

            Assert.Equal(expectedEvent.GetType(), actualEvent.GetType());
            Assert.Equal(JsonConvert.SerializeObject(expectedEvent), JsonConvert.SerializeObject(actualEvent));
        }
    }

    [Fact]
    public async Task CanSaveMemento()
    {
        var expectedMemento = new PostMemento(Guid.NewGuid(), 10, "test", "test", Guid.NewGuid());

        await _mementoStore.SaveAsync(expectedMemento);

        var actualMemento = await _mementoStore.GetLatestMementoAsync(expectedMemento.SourcedId);

        Assert.Equal(JsonConvert.SerializeObject(expectedMemento), JsonConvert.SerializeObject(actualMemento));
    }
}