using Funzone.Application.Contract;
using MediatR;

namespace Funzone.Application.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> :
        IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}