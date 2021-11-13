namespace MarchNote.Domain.Notes
{
    public record NoteMemberRole
    {
        public static NoteMemberRole Author => new NoteMemberRole(nameof(Author));
        public static NoteMemberRole Writer => new NoteMemberRole(nameof(Writer));
        public static NoteMemberRole Reader => new NoteMemberRole(nameof(Reader));
        public string Value { get; }

        private NoteMemberRole(string value)
        {
            Value = value;
        }

        public static NoteMemberRole Of(string value)
        {
            return new NoteMemberRole(value);
        }
    }
}