using System;
using System.Threading.Tasks;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.UnitTests.Spaces
{
    public class SpaceTests : TestBase
    {
        private Space _space;

        [SetUp]
        public void SetUp()
        {
            _space = SpaceTestUtil.CreateSpace();
        }
        
        [Test]
        public void CheckAuthor_ByNoAuthor_ThrowException()
        {
            ShouldThrowBusinessException(() => _space.CheckAuthor(Guid.NewGuid()),
                ExceptionCode.SpaceException,
                "Only author can operate space");
        }

        [Test]
        public void CheckDelete_HasBeenDeleted_ThrowException()
        {
            _space.SoftDelete(_space.AuthorId);

            ShouldThrowBusinessException(() => _space.SoftDelete(_space.AuthorId),
                ExceptionCode.SpaceException,
                "Space has been deleted");
        }

        [Test]
        public void Rename_IsSuccessful()
        {
            _space.Rename(_space.AuthorId, "test");
            _space.Name.ShouldBe("test");
        }

        [Test]
        public void Delete_IsSuccessful()
        {
            _space.SoftDelete(_space.AuthorId);
            _space.IsDeleted.ShouldBeTrue();
        }

        [Test]
        public void AddFolder_IsSuccessful()
        {
            var folderSpace = _space.AddFolder(_space.AuthorId, "folder");
            folderSpace.Type.ShouldBe(SpaceType.Folder);
            folderSpace.ParentId.ShouldBe(_space.Id);
            folderSpace.Name.ShouldBe("folder");
        }
    }
}