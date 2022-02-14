using System.Security.Claims;
using MediatR;
using SmartNote.Application.Configuration.Commands;
using SmartNote.Application.Configuration.Security.Users;
using SmartNote.Application.Users.Commands;
using SmartNote.Application.Users.Queries;
using SmartNote.Domain.Users;

namespace SmartNote.Application.Users.Handlers;

public class UserCommandHandler :
    IRequestHandler<AuthenticateCommand, AuthenticationResult>,
    ICommandHandler<RegisterUserCommand, Guid>,
    ICommandHandler<UpdateProfileCommand, Unit>,
    ICommandHandler<ChangePasswordCommand, Unit>
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserChecker _userChecker;
    private readonly IUserRepository _userRepository;

    public UserCommandHandler(
        ICurrentUser currentUser,
        IUserChecker userChecker,
        IUserRepository userRepository)
    {
        _currentUser = currentUser;
        _userChecker = userChecker;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(AuthenticateCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(u => u.Email == request.Email);

        if (user == null)
        {
            return new AuthenticationResult("Incorrect login or password");
        }

        if (!PasswordManager.VerifyHashedPassword(user.Password, request.Password))
        {
            return new AuthenticationResult("Incorrect login or password");
        }

        return new AuthenticationResult(new UserAuthenticateDto
        {
            Id = user.Id,
            Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            },
            Email = user.Email,
            IsActive = user.IsActive,
            FirstName = user.FirstName,
            LastName = user.LastName
        });
    }

    public async Task<Guid> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await User.Register(
            _userChecker,
            request.Email,
            PasswordManager.HashPassword(request.Password),
            request.FirstName,
            request.LastName);

        await _userRepository.InsertAsync(user);

        return user.Id;
    }

    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(_currentUser.Id);

        user.UpdateProfile(
            request.FirstName,
            request.LastName,
            request.Bio,
            request.Avatar);

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(_currentUser.Id);

        user.ChangePassword(
            _userChecker,
            request.OldPassword,
            PasswordManager.HashPassword(request.NewPassword));

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}