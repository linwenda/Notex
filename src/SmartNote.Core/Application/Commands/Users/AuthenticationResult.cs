using SmartNote.Core.Application.Dto;

namespace SmartNote.Core.Application.Commands.Users;

public class AuthenticationResult
{
    public AuthenticationResult(string authenticationError)
    {
        IsAuthenticated = false;
        AuthenticationError = authenticationError;
    }

    public AuthenticationResult(UserAuthenticateDto user)
    {
        this.IsAuthenticated = true;
        this.User = user;
    }

    public bool IsAuthenticated { get; }

    public string AuthenticationError { get; }

    public UserAuthenticateDto User { get; }
}