using MediatR;

namespace SmartNote.Core.Application
{
    public interface IQueryHandler<in TQuery, TResponse> :
        IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}