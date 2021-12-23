using System.Security.Claims;
using MediatR;
using SmartNote.Core.Application.Users.Commands;
using SmartNote.Core.Application.Users.Queries;
using SmartNote.Core.Domain;
using SmartNote.Core.Domain.Users;
using SmartNote.Core.Domain.Users.Exceptions;
using SmartNote.Core.Security;
using SmartNote.Core.Security.Users;

namespace SmartNote.Core.Application.Users.Handlers;

public class UserCommandHandler :
    ICommandHandler<AuthenticateCommand, UserAuthenticateDto>,
    ICommandHandler<RegisterUserCommand, Guid>,
    ICommandHandler<ChangePasswordCommand, Unit>,
    ICommandHandler<UpdateProfileCommand, Unit>
{
    private readonly IEncryptionService _encryptionService;
    private readonly ICurrentUser _currentUser;
    private readonly IUserChecker _userChecker;
    private readonly IRepository<User> _userRepository;

    public UserCommandHandler(
        ICurrentUser currentUser,
        IUserChecker userChecker,
        IEncryptionService encryptionService,
        IRepository<User> userRepository)
    {
        _currentUser = currentUser;
        _userChecker = userChecker;
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<UserAuthenticateDto> Handle(AuthenticateCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null) throw new IncorrectEmailOrPasswordException();

        user.CheckPassword(_encryptionService, request.Password);

        return new UserAuthenticateDto
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
        };
    }

    public async Task<Guid> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await User.Register(
            _userChecker,
            _encryptionService,
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName);

        await _userRepository.InsertAsync(user);

        return user.Id;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(_currentUser.Id);

        user.ChangePassword(_encryptionService, request.OldPassword, request.NewPassword);

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
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
}