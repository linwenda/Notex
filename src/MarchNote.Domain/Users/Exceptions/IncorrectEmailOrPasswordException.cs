﻿using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Users.Exceptions
{
    public class IncorrectEmailOrPasswordException : BusinessException
    {
        public IncorrectEmailOrPasswordException() : base(DomainErrorCodes.IncorrectEmailOrPassword,
            "Incorrect email address or password")
        {
        }
    }
}