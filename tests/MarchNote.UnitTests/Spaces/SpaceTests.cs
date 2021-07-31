using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Spaces;
using MarchNote.Domain.Users;
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
            _space = new Space(new UserId(Guid.NewGuid()), "space", "#FFF", "Bear");
        }
        
        [Test]
        public void Rename_ByNoAuthor_ThrowException()
        {
            ShouldThrowBusinessException(() => _space.Rename(new UserId(Guid.NewGuid()), "test"),
                ExceptionCode.SpaceCanBeOperatedOnlyByAuthor);
        }

        [Test]
        public void Rename_ByAuthor_IsSuccessful()
        {
            _space.Rename(_space.AuthorId, "test");
            _space.Name.ShouldBe("test");
        }

        [Test]
        public void Delete_ByAuthor_IsSuccessful()
        {
            _space.SoftDelete(_space.AuthorId);
            _space.IsDeleted.ShouldBeTrue();
        }

        [Test]
        public void Delete_HasBeenDeleted_ThrowException()
        {
            _space.SoftDelete(_space.AuthorId);

            ShouldThrowBusinessException(() => _space.SoftDelete(_space.AuthorId), 
                ExceptionCode.SpaceHasBeenDeleted);
        }
    }
}