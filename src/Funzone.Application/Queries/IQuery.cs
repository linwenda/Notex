using MediatR;

namespace Funzone.Application.Contract
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}