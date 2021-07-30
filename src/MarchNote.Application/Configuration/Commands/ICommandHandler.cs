﻿using MediatR;

namespace MarchNote.Application.Configuration.Commands
{
    public interface ICommandHandler<in TCommand, TResponse> :
        IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}