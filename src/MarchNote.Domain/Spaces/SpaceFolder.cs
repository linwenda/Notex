using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Spaces
{
    public class SpaceFolder : Entity
    {
        public SpaceFolderId Id { get; private set; }
        public SpaceId SpaceId { get; private set; }
        public UserId AuthorId { get; private set; }
        public SpaceFolderId ParentId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Name { get; private set; }

        private SpaceFolder()
        {
            //Only for EF
        }

        private SpaceFolder(SpaceId spaceId, UserId userId, string name, SpaceFolderId parentId)
        {
            Id = new SpaceFolderId(Guid.NewGuid());
            CreatedAt = DateTime.UtcNow;

            SpaceId = spaceId;
            AuthorId = userId;
            Name = name;
            ParentId = parentId;
        }

        internal static SpaceFolder CreateFolder(SpaceId spaceId, UserId userId, string name)
        {
            return new SpaceFolder(spaceId, userId, name, null);
        }

        public SpaceFolder AddSubFolder(UserId userId, string name)
        {
            CheckAuthor(userId);
            
            return new SpaceFolder(SpaceId, userId, name, Id);
        }

        public void Move(UserId userId, SpaceFolderId destinationFolderId)
        {
            CheckAuthor(userId);
            
            ParentId = destinationFolderId;
        }

        private void CheckAuthor(UserId userId)
        {
            if (userId != AuthorId)
            {
                throw new BusinessException(ExceptionCode.SpaceFolderCanBeOperatedOnlyByAuthor,
                    "Only author can operate folder");
            }
        }
    }
}