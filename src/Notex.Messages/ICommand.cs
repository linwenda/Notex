using MediatR;

namespace Notex.Messages
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : ICommand<Unit>
    {
    }
}