using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Notex.Core.Identity;
using Notex.Messages.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Notex.Core.Exceptions;
using Notex.Infrastructure.Settings;


namespace Notex.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly ICurrentUser _currentUser;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSetting _jwtSetting;

    public IdentityService(
        ICurrentUser currentUser,
        UserManager<ApplicationUser> userManager,
        JwtSetting jwtSetting)
    {
        _currentUser = currentUser;
        _userManager = userManager;
        _jwtSetting = jwtSetting;
    }

    public async Task<UserInfo> GetCurrentUserInfoAsync()
    {
        var user = await _userManager.FindByIdAsync(_currentUser.Id.ToString());

        var roles = await _userManager.GetRolesAsync(user);

        return new UserInfo
        {
            Id = user.Id,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            Surname = user.Surname,
            Avatar = user.Avatar,
            Bio = user.Bio,
            Roles = roles.ToArray()
        };
    }

    public async Task<string> GenerateTokenAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            throw new EntityNotFoundException(typeof(ApplicationUser), userName);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSetting.Secret);
        var roles = await _userManager.GetRolesAsync(user);

        var identityClaims = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        });

        identityClaims.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(identityClaims),
            Expires = DateTime.UtcNow.AddDays(365),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}