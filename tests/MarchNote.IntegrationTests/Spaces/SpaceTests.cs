using System;
using System.Linq;
using System.Threading.Tasks;
using MarchNote.Application.Spaces.Commands;
using MarchNote.Application.Spaces.Queries;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
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
                Visibility = Visibility.Public
            };

            var createSpaceResponse = await Send(createSpaceCommand);

            var getSpacesResponse = await Send(new GetDefaultSpacesQuery());
            getSpacesResponse.Data.Count().ShouldBe(1);
            getSpacesResponse.Data.Single().Id.ShouldBe(createSpaceResponse.Data);
            getSpacesResponse.Data.Single().Name.ShouldBe(createSpaceCommand.Name);
            getSpacesResponse.Data.Single().BackgroundColor.ShouldBe(createSpaceCommand.BackgroundColor);
            getSpacesResponse.Data.Single().BackgroundImageId.ShouldBe(createSpaceCommand.BackgroundImageId);
            getSpacesResponse.Data.Single().Type.ShouldBe(SpaceType.Default);
            getSpacesResponse.Data.Single().Visibility.ShouldBe(Visibility.Public);
            getSpacesResponse.Data.Single().ParentId.ShouldBeNull();
        }

        [Test]
        public async Task ShouldDeleteSpace()
        {
            var spaceId = await CreateDefaultSpace();

            await Send(new DeleteSpaceCommand(spaceId));

            var getSpacesResponse = await Send(new GetDefaultSpacesQuery());
            getSpacesResponse.Data.Count().ShouldBe(0);
        }

        [Test]
        public async Task ShouldRenameSpace()
        {
            var spaceId = await CreateDefaultSpace();

            await Send(new RenameSpaceCommand(spaceId, "space"));

            var getSpacesResponse = await Send(new GetDefaultSpacesQuery());
            getSpacesResponse.Data.Single().Name.ShouldBe("space");
        }

        [Test]
        public async Task ShouldAddFolderSpace()
        {
            var spaceId = await CreateDefaultSpace();

            var addFolderSpaceCommand = new AddFolderSpaceCommand(spaceId, "folder");
            var addFolderSpaceResponse = await Send(addFolderSpaceCommand);

            var getChildrenSpacesResponse = await Send(new GetFolderSpacesQuery(spaceId));

            getChildrenSpacesResponse.Data.Count().ShouldBe(1);
            getChildrenSpacesResponse.Data.Single().Id.ShouldBe(addFolderSpaceResponse.Data);
            getChildrenSpacesResponse.Data.Single().Name.ShouldBe(addFolderSpaceCommand.Name);
            getChildrenSpacesResponse.Data.Single().Type.ShouldBe(SpaceType.Folder);
            getChildrenSpacesResponse.Data.Single().ParentId.ShouldBe(spaceId);
        }

        [Test]
        public async Task ShouldMoveSpace()
        {
            var spaceId = await CreateDefaultSpace();

            var addChildFolderResponse = await Send(new AddFolderSpaceCommand(spaceId, "children"));
            var addParentFolderResponse = await Send(new AddFolderSpaceCommand(spaceId, "parent"));

            var childFolderId = addChildFolderResponse.Data;
            var parentFolderId = addParentFolderResponse.Data;

            await Send(new MoveSpaceCommand(
                childFolderId,
                parentFolderId));

            var getFolderSpacesResponse = await Send(new GetFolderSpacesQuery(parentFolderId));
            getFolderSpacesResponse.Data.Single().Id.ShouldBe(childFolderId);
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
            return createSpaceResponse.Data;
        }
    }
}