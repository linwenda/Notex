using System;
using Funzone.Services.Albums.Domain.PhotoAlbums;
using Funzone.Services.Albums.Domain.PhotoAlbums.Exceptions;
using Funzone.Services.Albums.Domain.Users;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Albums.UnitTests.PhotoAlbums
{
    public class AlbumTests
    {
        private IAlbumCounter _albumCounter;
        
        [SetUp]
        public void Setup()
        {
            _albumCounter = Substitute.For<IAlbumCounter>();
        }

        [Test]
        public void Create_WithUniqueName_Successful()
        {
            var name = "default";
            var userId = new UserId(Guid.NewGuid());
          
            var album =  Album.Create(name, userId, _albumCounter);
            album.Name.ShouldBe(name);
            album.UserId.ShouldBe(userId);
        }

        [Test]
        public void Create_WhenNameAlreadyExist_ThrowAlbumNameMustBeUniqueException()
        {
            _albumCounter.CountAlbumsWithName(Arg.Any<string>(), Arg.Any<UserId>())
                .Returns(1);

            Should.Throw<AlbumNameMustBeUniqueException>(() =>
                Album.Create("default", new UserId(Guid.NewGuid()), _albumCounter));
        }
    }
}