using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Exceptions;
using Funzone.Services.Albums.Application.Commands.ChangeVisibility;
using Funzone.Services.Albums.Domain.Albums;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.UnitTests.Albums
{
    public class ChangeVisibilityTests : TestBase
    {
        [Test(Description = "Handler unit testing")]
        public void MakePrivateHandler_NotFoundAlbum_ThrowNotFoundException()
        {
            var makePrivateCommandHandler = new MakePrivateCommandHandler(Substitute.For<IAlbumRepository>());

            Should.Throw<NotFoundException>(async () => await
                makePrivateCommandHandler.Handle(new MakePrivateCommand(Guid.NewGuid()), CancellationToken.None));
        }

        [Test(Description = "Handler unit testing")]
        public void MakePublicHandler_NotFoundAlbum_ThrowNotFoundException()
        {
            var makePublicCommandHandler = new MakePublicCommandHandler(Substitute.For<IAlbumRepository>());

            Should.Throw<NotFoundException>(async () =>
                await makePublicCommandHandler.Handle(new MakePublicCommand(Guid.NewGuid()), CancellationToken.None));
        }
    }
}