using MediatR;

namespace Funzone.BuildingBlocks.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
