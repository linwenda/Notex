using MediatR;
using Notex.Core.Aggregates.Users.DomainServices;
using Notex.Core.Configuration;
using Notex.Messages.Users.Commands;

namespace Notex.Core.Aggregates.Users.CommandHandlers;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IPasswordService _passwordService;
    private readonly IAggregateRepository _aggregateRepository;

    public ChangePasswordCommandHandler(
        ICurrentUser currentUser,
        IPasswordService passwordService,
        IAggregateRepository aggregateRepository)
    {
        _currentUser = currentUser;
        _passwordService = passwordService;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _aggregateRepository.LoadAsync<User>(_currentUser.Id);

        user.ChangePassword(_passwordService, request.OldPassword, request.NewPassword);

        await _aggregateRepository.SaveAsync(user);

        return Unit.Value;
    }
}