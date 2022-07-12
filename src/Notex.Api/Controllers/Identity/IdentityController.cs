using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notex.Core.Identity;
using Notex.Infrastructure.Identity;

namespace Notex.Api.Controllers.Identity;

[Authorize]
[Route("identity")]
public class IdentityController : ControllerBase
{
    private readonly ICurrentUser _currentUser;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityService _identityService;

    public IdentityController(
        ICurrentUser currentUser,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IIdentityService identityService)
    {
        _currentUser = currentUser;
        _userManager = userManager;
        _signInManager = signInManager;
        _identityService = identityService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest request)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

        var response = new AuthenticateResponse
        {
            Succeeded = signInResult.Succeeded,
            IsLockedOut = signInResult.IsLockedOut,
            IsNotAllowed = signInResult.IsNotAllowed,
            RequiresTwoFactor = signInResult.RequiresTwoFactor
        };

        if (signInResult.Succeeded)
        {
            response.AccessToken = await _identityService.GenerateTokenAsync(request.Email);
        }

        return Ok(response);
    }

    [Route("me")]
    [HttpGet]
    public async Task<IActionResult> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        return Ok(await _identityService.GetCurrentUserInfoAsync());
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
    {
        var createResult = await _userManager.CreateAsync(new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            Surname = request.Surname
        }, request.Password);

        return Ok(new RegisterUserResponse
        {
            Succeeded = createResult.Succeeded,
            Errors = createResult.Errors.Select(e => e.Description).ToArray()
        });
    }

    [HttpPut("password")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(_currentUser.Id.ToString());

        var changePasswordResult =
            await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        return Ok(new ChangePasswordResponse
        {
            Succeeded = changePasswordResult.Succeeded,
            Errors = changePasswordResult.Errors.Select(e => e.Description).ToArray()
        });
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateProfileRequest request)
    {
        var user = await _userManager.FindByIdAsync(_currentUser.Id.ToString());

        user.Surname = request.Surname;
        user.Avatar = request.Avatar;
        user.Bio = request.Bio;

        await _userManager.UpdateAsync(user);

        return Ok(new UpdateProfileResponse { Succeeded = true });
    }
}