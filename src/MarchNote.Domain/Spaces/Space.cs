using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Spaces
{
    public class Space : Entity
    {
        public SpaceId Id { get; private set; }
        public SpaceId ParentId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public UserId AuthorId { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        public string Icon { get; private set; }
        public SpaceType Type { get; private set; }
        public bool IsDeleted { get; private set; }
        public Visibility Visibility { get; private set; }

        private Space()
        {
            //Only for EF
        }

        private Space(SpaceId parentId, UserId userId, string name, string color, string icon, SpaceType type)
        {
            Id = new SpaceId(Guid.NewGuid());
            CreatedAt = DateTime.UtcNow;
            ParentId = parentId;
            AuthorId = userId;
            Name = name;
            Color = color;
            Icon = icon;
            Type = type;
            Visibility = Visibility.Public;
        }

        public static Space Create(UserId userId, string name, string color, string icon)
        {
            return new Space(null, userId, name, color, icon, SpaceType.Default);
        }

        public Space AddFolder(UserId userId, string name)
        {
            CheckDelete();
            CheckAuthor(userId, "Only author can add folder");

            return new Space(Id, userId, name, "", "", SpaceType.Folder);
        }

        public void Move(UserId userId, Space destSpace)
        {
            CheckDelete();
            CheckAuthor(userId);
            destSpace.CheckDelete();
            destSpace.CheckAuthor(userId);

            if (ParentId == destSpace.Id)
            {
                return;
            }

            if (Type != SpaceType.Folder)
            {
                throw new SpaceException("Only folder type can be moved");
            }

            if (Id == destSpace.Id)
            {
                throw new SpaceException("Invalid move");
            }

            ParentId = destSpace.Id;
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

        public void CheckAuthor(UserId userId, string message = "")
        {
            if (userId != AuthorId)
            {
                throw new SpaceException(string.IsNullOrWhiteSpace(message)
                    ? "Only author can operate space"
                    : message);
            }
        }

        public void CheckDelete()
        {
            if (IsDeleted)
            {
                throw new SpaceException("Space has been deleted");
            }
        }
    }
}