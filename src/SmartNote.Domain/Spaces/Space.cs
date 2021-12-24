using SmartNote.Domain.Notes;
using SmartNote.Domain.Spaces.Exceptions;

namespace SmartNote.Domain.Spaces
{
    public sealed class Space : Entity<Guid>, IHasCreationTime
    {
        public Guid? ParentId { get; private set; }
        public DateTimeOffset CreationTime { get; set; }
        public Guid AuthorId { get; private set; }
        public string Name { get; private set; }
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
            Visibility visibility)
        {
            ParentId = parentId;
            AuthorId = userId;
            Name = name;
            Background = background;
            Type = type;
            Visibility = visibility;
        }

        public static async Task<Space> Create(
            ISpaceChecker spaceChecker,
            Guid userId,
            string name,
            Background background,
            Visibility visibility)
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
                visibility);
        }

        public async Task Update(ISpaceChecker spaceChecker, string name, Visibility visibility,
            Guid? backgroundImageId)
        {
            if (!await spaceChecker.IsUniqueNameAsync(AuthorId, Id, name))
            {
                throw new SpaceNameAlreadyExistsException();
            }

            Name = name;
            Visibility = visibility;

            if (backgroundImageId != null)
            {
                Background = new Background("", backgroundImageId.Value);
            }
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
                Visibility.Public);
        }

        public void SoftDelete(Guid userId)
        {
            CheckDelete();
            CheckAuthor(userId);

            IsDeleted = true;
        }

        private void CheckAuthor(Guid userId)
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

        public Note CreateNote(
            Guid userId,
            string title)
        {
            CheckDelete();
            CheckAuthor(userId);

            return Note.Create(Id,
                userId,
                title);
        }
    }
}