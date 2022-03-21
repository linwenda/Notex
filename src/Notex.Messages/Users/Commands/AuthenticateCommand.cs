using Notex.Messages.Users.Dtos;

namespace Notex.Messages.Users.Commands;

public class AuthenticateCommand : ICommand<AuthenticateResult>
{
    public AuthenticateCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }
    public string Password { get; }
}

public class AuthenticateResult
{
    public AuthenticateResult(string authenticationError)
    {
        IsAuthenticated = false;
        AuthenticationError = authenticationError;
    }

    public AuthenticateResult(UserDto user)
    {
        IsAuthenticated = true;
        User = user;
    }

    public bool IsAuthenticated { get; }

    public string AuthenticationError { get; }

    public UserDto User { get; }
}