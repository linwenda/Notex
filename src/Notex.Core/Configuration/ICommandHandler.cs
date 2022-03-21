using MediatR;
using Notex.Messages;

namespace Notex.Core.Configuration;

public interface ICommandHandler<in TCommand, TResponse> :
    IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}

public interface ICommandHandler<in TCommand> :
    IRequestHandler<TCommand>
    where TCommand : ICommand
{
}

