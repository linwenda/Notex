using System.Threading;
using System.Threading.Tasks;
using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Infrastructure;
using MediatR;

namespace Funzone.Services.Albums.Application.Configurations.Behaviours
{
    public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehaviour(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            await _unitOfWork.CommitAsync(cancellationToken);
            return response;
        }
    }

    public class UnitOfWorkWithResultBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkWithResultBehaviour(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            await _unitOfWork.CommitAsync(cancellationToken);
            return response;
        }
    }
}