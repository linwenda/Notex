using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.Users.Commands;

namespace Notex.Core.Aggregates.Users.CommandHandlers;

public class UpdateUserRolesCommandHandler : ICommandHandler<UpdateUserRolesCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public UpdateUserRolesCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await _aggregateRepository.LoadAsync<User>(request.UserId);

        user.UpdateRoles(request.Roles);

        await _aggregateRepository.SaveAsync(user);

        return Unit.Value;
    }
}