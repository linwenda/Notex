using MediatR;

namespace SmartNote.Core.Application
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}