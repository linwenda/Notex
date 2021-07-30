using System;

namespace MarchNote.Domain.SeedWork
{
    public class BusinessException : Exception
    {
        public ExceptionCode Code { get; }

        public BusinessException(ExceptionCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}