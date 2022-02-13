using System.Security.Claims;
using MediatR;
using SmartNote.Core.Application.Dto;
using SmartNote.Core.Domain.Users;

namespace SmartNote.Core.Application.Commands.Users;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticationResult>
{
    private readonly IUserManager _userManager;
    private readonly IUserRepository _userRepository;

    public AuthenticateCommandHandler(IUserManager userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(u => u.Email == request.Email);

        if (user == null)
        {
            return new AuthenticationResult("Incorrect login or password");
        }

        if (!_userManager.VerifyHashedPassword(user.HashedPassword, request.Password))
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
            FirstName = user.FirstName,
            LastName = user.LastName
        });
    }
}