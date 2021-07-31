﻿using System;
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
            _space = Space.Create(new UserId(Guid.NewGuid()), "space", "#FFF", "Bear");
        }

        [Test]
        public void CheckAuthor_ByNoAuthor_ThrowException()
        {
            ShouldThrowBusinessException(() => _space.CheckAuthor(new UserId(Guid.NewGuid())),
                ExceptionCode.SpaceCanBeOperatedOnlyByAuthor);
        }

        [Test]
        public void CheckDelete_HasBeenDeleted_ThrowException()
        {
            _space.SoftDelete(_space.AuthorId);

            ShouldThrowBusinessException(() => _space.CheckDelete(),
                ExceptionCode.SpaceHasBeenDeleted);
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


        [Test]
        public void Move_NotFolderType_ThrowException()
        {
            var newSpace = Space.Create(_space.AuthorId, "name", "", "");

            ShouldThrowBusinessException(() => _space.Move(_space.AuthorId, newSpace),
                ExceptionCode.SpaceOnlyFolderTypeCanBeMoved);
        }

        [Test]
        public void Move_WhenMovingOneself_ThrowException()
        {
            var folderSpace = _space.AddFolder(_space.AuthorId, "folder");

            ShouldThrowBusinessException(() => folderSpace.Move(folderSpace.AuthorId, folderSpace),
                ExceptionCode.SpaceCannotMovingOneself);
        }

        [Test]
        public void Move_Folder_IsSuccessful()
        {
            var folderSpace = _space.AddFolder(_space.AuthorId, "folder");
            var folderSpace2 = _space.AddFolder(_space.AuthorId, "folder");

            folderSpace.Move(folderSpace.AuthorId, folderSpace2);
            folderSpace.ParentId.ShouldBe(folderSpace2.Id);
        }
    }
}