using MediatR;

namespace Funzone.Application.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}