using MediatR;

namespace Notex.Messages;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}