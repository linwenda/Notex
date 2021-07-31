using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.UnitTests.Spaces
{
    public class SpaceFolderTests : TestBase
    {
        private SpaceFolder _folder;

        [SetUp]
        public void SetUp()
        {
            var space = new Space(new UserId(Guid.NewGuid()), "space", "#FFF", "Bear");
            _folder = space.AddFolder(space.AuthorId, "2021-7-30");
        }

        [Test]
        public void AddSubFolder_ByAuthor_IsSuccessful()
        {
            var subFolder = _folder.AddSubFolder(_folder.AuthorId, "sub");
            subFolder.ParentId.ShouldBe(_folder.Id);
            subFolder.AuthorId.ShouldBe(_folder.AuthorId);
            subFolder.SpaceId.ShouldBe(_folder.SpaceId);
            subFolder.Name.ShouldBe("sub");
        }

        [Test]
        public void AddSubFolder_ByNoAuthor_ThrowException()
        {
            ShouldThrowBusinessException(() => _folder.AddSubFolder(new UserId(Guid.NewGuid()), "sub"),
                ExceptionCode.SpaceFolderCanBeOperatedOnlyByAuthor);
        }

        [Test]
        public void Move_ByAuthor_IsSuccessful()
        {
            var destId = new SpaceFolderId(Guid.NewGuid());

            _folder.Move(_folder.AuthorId, destId);
            _folder.ParentId.ShouldBe(destId);

            _folder.Move(_folder.AuthorId, null);
            _folder.ParentId.ShouldBeNull();
        }
    }
}