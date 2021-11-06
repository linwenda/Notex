﻿using System;

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

    public class BusinessNewException : Exception
    {
        public string Code { get; }
        public string Details { get; }

        public BusinessNewException(
            string code,
            string message,
            string details = null,
            Exception innerException = null) :
            base(message, innerException)
        {
            Code = code;
            Details = details;
        }

        public BusinessNewException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}