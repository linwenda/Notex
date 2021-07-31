using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Spaces
{
    public class Space : Entity
    {
        public SpaceId Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public UserId AuthorId { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        public string Icon { get; private set; }
        public bool IsDeleted { get; private set; }

        private Space()
        {
            //Only for EF
        }

        public Space(UserId userId, string name, string color, string icon)
        {
            Id = new SpaceId(Guid.NewGuid());
            CreatedAt = DateTime.UtcNow;
            AuthorId = userId;
            Name = name;
            Color = color;
            Icon = icon;
        }

        public void Rename(UserId userId, string name)
        {
            CheckDelete();
            CheckAuthor(userId, "Only author can rename space");

            Name = name;
        }

        public void SoftDelete(UserId userId)
        {
            CheckDelete();
            CheckAuthor(userId, "Only author can delete space");

            IsDeleted = true;
        }

        public SpaceFolder AddFolder(UserId userId, string name)
        {
            CheckAuthor(userId, "Only author can add folder");

            return SpaceFolder.CreateFolder(Id, AuthorId, name);
        }

        private void CheckAuthor(UserId userId, string message = "")
        {
            if (userId != AuthorId)
            {
                throw new BusinessException(ExceptionCode.SpaceCanBeOperatedOnlyByAuthor,
                    string.IsNullOrWhiteSpace(message) ? "Only author can operate space" : message);
            }
        }

        private void CheckDelete()
        {
            if (IsDeleted)
            {
                throw new BusinessException(ExceptionCode.SpaceHasBeenDeleted, "Space has been deleted");
            }
        }
    }
}