using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartNote.Application.Spaces.Commands;
using SmartNote.Application.Spaces.Queries;
using SmartNote.Domain.Spaces;

namespace SmartNote.IntegrationTests.Spaces
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
            };

            var createSpaceResponse = await Send(createSpaceCommand);

            var getSpacesResponse = await Send(new GetDefaultSpacesQuery());
            getSpacesResponse.Count().ShouldBe(1);

            var space = getSpacesResponse.First();

            space.Id.ShouldBe(createSpaceResponse);
            space.Name.ShouldBe(createSpaceCommand.Name);
            space.BackgroundColor.ShouldBe(createSpaceCommand.BackgroundColor);
            space.BackgroundImageId.ShouldBe(createSpaceCommand.BackgroundImageId);
            space.Type.ShouldBe(SpaceType.Default);
            space.Visibility.ShouldBe(Visibility.Public);
            space.ParentId.ShouldBeNull();
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