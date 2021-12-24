using MarchNote.UnitTests;
using NUnit.Framework;
using Shouldly;
using SmartNote.Domain.Spaces;
using SmartNote.Domain.Spaces.Exceptions;

namespace SmartNote.UnitTests.Spaces
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
        public void CheckDelete_HasBeenDeleted_ThrowException()
        {
            _space.SoftDelete(_space.AuthorId);

            Should.Throw<SpaceHasBeenDeletedException>(() => _space.SoftDelete(_space.AuthorId));
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