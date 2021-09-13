using System;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Attachments
{
    public class AttachmentId : TypedIdValueBase
    {
        public AttachmentId(Guid value) : base(value)
        {
        }
    }
}