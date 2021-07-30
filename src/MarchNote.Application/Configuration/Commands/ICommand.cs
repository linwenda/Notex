using MediatR;

namespace MarchNote.Application.Configuration.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}