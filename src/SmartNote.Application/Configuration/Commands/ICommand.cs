using MediatR;

namespace SmartNote.Application.Configuration.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}