using System;
using System.Linq;
using System.Threading.Tasks;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Application.Spaces.Queries;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
using NSubstitute.Extensions;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.IntegrationTests.Spaces
{
    using static TestFixture;

    public class SpaceTests : TestBase
    {
        [Test]
        public async Task ShouldCreateSpace()
        {
            var createSpaceCommand = new CreateSpaceCommand
            {
                Name = "Family",
                BackgroundColor = "#FFF",
                Visibility = Visibility.Public,
                Description = "My favorite"
            };

            var createSpaceResponse = await Send(createSpaceCommand);

            var getSpacesResponse = await Send(new GetDefaultSpacesQuery());
            getSpacesResponse.Count().ShouldBe(1);
            getSpacesResponse.Single().Id.ShouldBe(createSpaceResponse);
            getSpacesResponse.Single().Name.ShouldBe(createSpaceCommand.Name);
            getSpacesResponse.Single().BackgroundColor.ShouldBe(createSpaceCommand.BackgroundColor);
            getSpacesResponse.Single().BackgroundImageId.ShouldBe(createSpaceCommand.BackgroundImageId);
            getSpacesResponse.Single().Type.ShouldBe(SpaceType.Default);
            getSpacesResponse.Single().Visibility.ShouldBe(Visibility.Public);
            getSpacesResponse.Single().ParentId.ShouldBeNull();
            getSpacesResponse.Single().Description.ShouldBe(createSpaceCommand.Description);
        }

        [Test]
        public async Task ShouldDeleteSpace()
        {
            var spaceId = await CreateDefaultSpace();

            await Send(new DeleteSpaceCommand(spaceId));

            var getSpacesResponse = await Send(new GetDefaultSpacesQuery());
            getSpacesResponse.Count().ShouldBe(0);
        }

        [Test]
        public async Task ShouldRenameSpace()
        {
            var spaceId = await CreateDefaultSpace();

            await Send(new RenameSpaceCommand(spaceId, "space"));

            var getSpacesResponse = await Send(new GetDefaultSpacesQuery());
            getSpacesResponse.Single().Name.ShouldBe("space");
        }

        [Test]
        public async Task ShouldAddFolderSpace()
        {
            var spaceId = await CreateDefaultSpace();

            var addFolderSpaceCommand = new AddFolderSpaceCommand(spaceId, "folder");
            var addFolderSpaceResponse = await Send(addFolderSpaceCommand);

            var getChildrenSpacesResponse = await Send(new GetFolderSpacesQuery(spaceId));

            getChildrenSpacesResponse.Count().ShouldBe(1);
            getChildrenSpacesResponse.Single().Id.ShouldBe(addFolderSpaceResponse);
            getChildrenSpacesResponse.Single().Name.ShouldBe(addFolderSpaceCommand.Name);
            getChildrenSpacesResponse.Single().Type.ShouldBe(SpaceType.Folder);
            getChildrenSpacesResponse.Single().ParentId.ShouldBe(spaceId);
        }

        private static async Task<Guid> CreateDefaultSpace()
        {
            var createSpaceCommand = new CreateSpaceCommand
            {
                Name = "Family",
                BackgroundColor = "#FFF",
                Visibility = Visibility.Public
            };

            var createSpaceResponse = await Send(createSpaceCommand);
            return createSpaceResponse;
        }
    }
}