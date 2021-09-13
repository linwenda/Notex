using MarchNote.Domain.SeedWork;

namespace MarchNote.Domain.Spaces
{
    public class SpaceException : BusinessException
    {
        public SpaceException(string message) : base(ExceptionCode.SpaceException, message)
        {
        }
    }
}