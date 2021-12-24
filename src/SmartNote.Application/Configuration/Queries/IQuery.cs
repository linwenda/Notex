using MediatR;

namespace SmartNote.Application.Configuration.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}