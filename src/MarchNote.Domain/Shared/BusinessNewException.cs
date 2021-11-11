using System;

namespace MarchNote.Domain.Shared
{
    public class BusinessNewException : Exception
    {
        public string Code { get; }

        public BusinessNewException(
            string code,
            string message) :
            base(message)
        {
            Code = code;
        }

        public BusinessNewException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}