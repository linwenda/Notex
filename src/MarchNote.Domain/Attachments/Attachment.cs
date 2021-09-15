using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Attachments
{
    public class Attachment : Entity
    {
        public AttachmentId Id { get; private set; }
        public DateTime UploadedAt { get; private set; }
        public UserId UploaderId { get; private set; }
        public string DisplayName { get; private set; }
        public string StoredName { get; private set; }
        public string Path { get; private set; }
        public string ContentType { get; private set; }

        private Attachment()
        {
        }

        public Attachment(
            UserId userId,
            string displayName,
            string storedName,
            string path,
            string contentType)
        {
            Id = new AttachmentId(Guid.NewGuid());
            UploadedAt = DateTime.UtcNow;
            UploaderId = userId;
            DisplayName = displayName;
            StoredName = storedName;
            Path = path;
            ContentType = contentType;
        }
    }
}