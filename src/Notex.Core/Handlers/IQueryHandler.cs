using MediatR;
using Notex.Messages;

namespace Notex.Core.Handlers;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}