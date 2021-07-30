using System;

namespace MarchNote.Application.Configuration.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Forbidden Access")
        {
        }
    }
}