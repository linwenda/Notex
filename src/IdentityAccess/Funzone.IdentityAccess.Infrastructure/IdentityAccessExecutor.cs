using Autofac;
using Funzone.BuildingBlocks.Application.Commands;
using MediatR;
using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Infrastructure
{
    public static class IdentityAccessExecutor
    {
        public static async Task Execute(ICommand command)
        {
            using (var scope = IdentityAccessCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }
    }
}
