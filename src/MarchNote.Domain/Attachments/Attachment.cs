using System;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;

namespace MarchNote.Domain.Attachments
{
    public class Attachment : Entity
    {
        public AttachmentId Id { get; private set; }
        public DateTime UploadedAt { get; private set; }
        public UserId UploadedBy { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }

        private Attachment()
        {
        }

        public Attachment(UserId userId, string name, string path)
        {
            Id = new AttachmentId(Guid.NewGuid());
            UploadedAt = DateTime.UtcNow;
            UploadedBy = userId;
            Name = name;
            Path = path;
        }
    }
}