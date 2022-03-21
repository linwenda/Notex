using MediatR;
using Notex.Core.Configuration;
using Notex.Messages.Users.Commands;

namespace Notex.Core.Aggregates.Users.CommandHandlers;

public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand>
{
    private readonly IAggregateRepository _aggregateRepository;
    private readonly ICurrentUser _currentUser;

    public UpdateProfileCommandHandler(IAggregateRepository aggregateRepository, ICurrentUser currentUser)
    {
        _aggregateRepository = aggregateRepository;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _aggregateRepository.LoadAsync<User>(_currentUser.Id);

        user.UpdateProfile(request.Name, request.Avatar, request.Bio);

        await _aggregateRepository.SaveAsync(user);

        return Unit.Value;
    }
}