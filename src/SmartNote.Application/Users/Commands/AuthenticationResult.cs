using SmartNote.Application.Users.Queries;

namespace SmartNote.Application.Users.Commands;

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