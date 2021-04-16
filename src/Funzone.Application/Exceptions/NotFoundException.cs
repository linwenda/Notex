using System;

namespace Funzone.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base()
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}