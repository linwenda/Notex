using System;

namespace MarchNote.Application.Configuration.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(Type entityType, object key) : base(
            $"Entity \"{entityType.FullName}\" ({key}) was not found.")
        {
        }
    }
}