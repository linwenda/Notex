using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarchNote.Domain.Notes;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Shared;
using MarchNote.Domain.Spaces.Exceptions;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Spaces
{
    public sealed class Space : Entity<Guid>
    {
        public Guid? ParentId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid AuthorId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Background Background { get; private set; }
        public SpaceType Type { get; private set; }
        public bool IsDeleted { get; private set; }
        public Visibility Visibility { get; private set; }

        private Space()
        {
            //Only for EF
        }

        private Space(
            Guid? parentId,
            Guid userId,
            string name,
            Background background,
            SpaceType type,
            Visibility visibility,
            string description)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            ParentId = parentId;
            AuthorId = userId;
            Name = name;
            Background = background;
            Type = type;
            Visibility = visibility;
            Description = description;
        }


        public static async Task<Space> Create(
            ISpaceChecker spaceChecker,
            Guid userId,
            string name,
            Background background,
            Visibility visibility,
            string description)
        {
            if (!await spaceChecker.IsUniqueNameAsync(userId, name))
            {
                throw new SpaceNameAlreadyExistsException();
            }

            return new Space(
                null,
                userId,
                name,
                background,
                SpaceType.Default,
                visibility, description);
        }

        public Space AddFolder(Guid userId, string name)
        {
            CheckDelete();
            CheckAuthor(userId);

            return new Space(
                Id,
                userId,
                name,
                new Background(),
                SpaceType.Folder,
                Visibility.Public, "");
        }

        public void Rename(Guid userId, string name)
        {
            CheckDelete();
            CheckAuthor(userId);

            Name = name;
        }

        public void SoftDelete(Guid userId)
        {
            CheckDelete();
            CheckAuthor(userId);

            IsDeleted = true;
        }

        public void CheckAuthor(Guid userId)
        {
            if (userId != AuthorId)
            {
                throw new NotAuthorOfTheSpaceException();
            }
        }

        private void CheckDelete()
        {
            if (IsDeleted)
            {
                throw new SpaceHasBeenDeletedException();
            }
        }

        public void SetBackground(Background background)
        {
            Background = background;
        }

        public Note CreateNote(
            Guid userId, 
            string title, 
            string content,
            List<string> tags)
        {
            CheckDelete();
            CheckAuthor(userId);
            
            return Note.Create(Id, 
                userId, 
                title,
                content,
                tags);
        }
    }
}