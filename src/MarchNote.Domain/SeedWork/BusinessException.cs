using System;

namespace MarchNote.Domain.SeedWork
{
    public abstract class BusinessException : Exception
    {
        public ExceptionCode Code { get; }

        protected BusinessException(ExceptionCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}