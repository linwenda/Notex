using SmartNote.Core.Ddd;

namespace SmartNote.Domain.Users.Exceptions
{
    public class IncorrectOldPasswordException : BusinessException
    {
        public IncorrectOldPasswordException() : base(DomainErrorCodes.IncorrectOldPassword,
            "The old password is incorrect")
        {
        }
    }
}