using System;

namespace MarchNote.Domain.Shared
{
    public class BusinessException : Exception
    {
        public string Code { get; }

        public BusinessException(
            string code,
            string message) :
            base(message)
        {
            Code = code;
        }

        public BusinessException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}