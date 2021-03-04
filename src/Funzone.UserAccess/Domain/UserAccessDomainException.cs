using System;

namespace Funzone.UserAccess.Domain
{
    public class UserAccessDomainException : Exception
    {
        public UserAccessDomainException()
        { }

        public UserAccessDomainException(string message)
            : base(message)
        { }

        public UserAccessDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}