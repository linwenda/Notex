using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Users
{
    public class UserException : BusinessException
    {
        public UserException(string message) : base(ExceptionCode.UserException, message)
        {
        }
    }
}