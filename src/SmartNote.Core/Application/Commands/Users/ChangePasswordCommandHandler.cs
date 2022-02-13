using MediatR;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Users;
using SmartNote.Core.Shared;

namespace SmartNote.Core.Application.Commands.Users;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserManager _userManager;
    private readonly ICurrentUser _currentUser;

    public ChangePasswordCommandHandler(
        IUserRepository userRepository,
        IUserManager userManager,
        ICurrentUser currentUser)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _currentUser = currentUser;
    }
    
    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(_currentUser.Id);

        user.ChangePassword(
            _userManager,
            request.OldPassword,
            PasswordManager.HashPassword(request.NewPassword));

        await _userRepository.UpdateAsync(user);
        
        return Unit.Value;
    }
}