using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Notex.Core.Aggregates.Users.DomainServices;
using Notex.Core.Aggregates.Users.ReadModels;
using Notex.Core.Configuration;
using Notex.Messages.Users.Commands;
using Notex.Messages.Users.Dtos;

namespace Notex.Core.Aggregates.Users.CommandHandlers;

public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, AuthenticateResult>
{
    private readonly IPasswordService _passwordService;
    private readonly IReadModelRepository _readModelRepository;

    public AuthenticateCommandHandler(IPasswordService passwordService, IReadModelRepository readModelRepository)
    {
        _passwordService = passwordService;
        _readModelRepository = readModelRepository;
    }

    public async Task<AuthenticateResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var user = await _readModelRepository.Query<UserDetail>()
            .Where(u => EF.Functions.ILike(u.Email, request.Email))
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return new AuthenticateResult("Incorrect login or password");
        }

        if (!_passwordService.VerifyHashedPassword(user.Password, request.Password))
        {
            return new AuthenticateResult("Incorrect login or password");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            },
            Email = user.Email,
            IsActive = user.IsActive,
            Name = user.Name
        };

        if (user.Roles.Any())
        {
            userDto.Claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));
        }

        return new AuthenticateResult(userDto);
    }
}