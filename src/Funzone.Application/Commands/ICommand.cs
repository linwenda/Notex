using MediatR;

namespace Funzone.Application.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}