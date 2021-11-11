using System;
using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Attachments
{
    public sealed class Attachment : Entity<Guid>
    {
        public DateTime UploadedAt { get; private set; }
        public Guid UploaderId { get; private set; }
        public string DisplayName { get; private set; }
        public string StoredName { get; private set; }
        public string Path { get; private set; }
        public string ContentType { get; private set; }

        private Attachment()
        {
        }

        public Attachment(
            Guid userId,
            string displayName,
            string storedName,
            string path,
            string contentType)
        {
            Id = Guid.NewGuid();
            UploadedAt = DateTime.UtcNow;
            UploaderId = userId;
            DisplayName = displayName;
            StoredName = storedName;
            Path = path;
            ContentType = contentType;
        }
    }
}