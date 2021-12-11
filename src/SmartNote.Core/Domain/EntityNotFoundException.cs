using System;

namespace SmartNote.Core.Domain
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(Type entityType) : base($"Entity \"{entityType.FullName}\" was not found.")
        {
        }

        public EntityNotFoundException(Type entityType, object key) : base(
            $"Entity \"{entityType.FullName}\" ({key}) was not found.")
        {
        }
    }
}