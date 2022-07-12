using System;
using System.Threading.Tasks;
using Notex.Core.Domain.SeedWork;
using Notex.IntegrationTests.EventSourcing.Domain;
using Xunit;

namespace Notex.IntegrationTests.EventSourcing;

[Collection("Sequence")]
public class EventSourcedRepositoryTests : IClassFixture<StartupFixture>
{
    private readonly IEventSourcedRepository<Post> _postRepository;
    private readonly IMementoStore _mementoStore;

    public EventSourcedRepositoryTests(StartupFixture fixture)
    {
        _postRepository = fixture.GetService<IEventSourcedRepository<Post>>();
        _mementoStore = fixture.GetService<IMementoStore>();
    }
    
    [Fact]
    public async Task CanLoadEventSourcedByHistory()
    {
        var post = Post.Initialize(Guid.NewGuid(), "test", "test");

        post.Edit("test2", "test2");

        await _postRepository.SaveAsync(post);

        var postFromHistory = await _postRepository.FindAsync(post.Id);
        Assert.Equal(2, postFromHistory.Version);
    }

    [Fact]
    public async Task CanTakeEventSourcedSnapshot()
    {
        var post = Post.Initialize(Guid.NewGuid(), "test", "test");

        post.Edit("test2", "test2");
        await _postRepository.SaveAsync(post);

        post.Edit("test3", "test3"); //will take snapshot
        await _postRepository.SaveAsync(post);
        
        post.Edit("test4", "test4");
        await _postRepository.SaveAsync(post);

        var postMemento = await _mementoStore.GetLatestMementoAsync(post.Id) as PostMemento;

        Assert.NotNull(postMemento);
        Assert.Equal(3, postMemento.Version);

        var postFromMemento = await _postRepository.FindAsync(post.Id);
        Assert.Equal(4, postFromMemento.Version);
    }
}