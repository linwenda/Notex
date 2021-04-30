using MediatR;

namespace Funzone.Application.Configuration.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}