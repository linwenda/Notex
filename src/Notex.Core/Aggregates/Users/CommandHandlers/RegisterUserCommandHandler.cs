using Notex.Core.Aggregates.Users.DomainServices;
using Notex.Core.Configuration;
using Notex.Messages.Users.Commands;

namespace Notex.Core.Aggregates.Users.CommandHandlers;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserChecker _userChecker;
    private readonly IPasswordService _passwordService;
    private readonly IAggregateRepository _aggregateRepository;

    public RegisterUserCommandHandler(
        IUserChecker userChecker,
        IPasswordService passwordService,
        IAggregateRepository aggregateRepository)
    {
        _userChecker = userChecker;
        _passwordService = passwordService;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Initialize(_userChecker, _passwordService, request.Email, request.Password,
            request.Name);

        await _aggregateRepository.SaveAsync(user);

        return user.Id;
    }
}