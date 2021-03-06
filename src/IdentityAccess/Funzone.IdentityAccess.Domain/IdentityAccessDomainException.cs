using System;

namespace Funzone.IdentityAccess.Domain
{
    public class IdentityAccessDomainException : Exception
    {
        public IdentityAccessDomainException()
        { }

        public IdentityAccessDomainException(string message)
            : base(message)
        { }

        public IdentityAccessDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}