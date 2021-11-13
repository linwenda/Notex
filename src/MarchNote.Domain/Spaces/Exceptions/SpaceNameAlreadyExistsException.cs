﻿using MarchNote.Domain.Shared;

namespace MarchNote.Domain.Spaces.Exceptions
{
    public class SpaceNameAlreadyExistsException : BusinessException
    {
        public SpaceNameAlreadyExistsException() : base(DomainErrorCodes.SpaceNameAlreadyExists,
            "Space with this name already exists")
        {
        }
    }
}