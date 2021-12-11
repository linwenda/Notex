using MediatR;

namespace SmartNote.Core.Application
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}