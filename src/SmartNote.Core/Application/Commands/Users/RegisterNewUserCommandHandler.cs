using MediatR;
using SmartNote.Core.Domain.Users;

namespace SmartNote.Core.Application.Commands.Users;

public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Guid>
{
    private readonly IUserManager _userManager;
    private readonly IUserRepository _userRepository;

    public RegisterNewUserCommandHandler(IUserManager userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = await User.RegisterAsync(_userManager,
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName);

        await _userRepository.InsertAsync(user);

        return user.Id;
    }
}