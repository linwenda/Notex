using MediatR;

namespace SmartNote.Core.Application.Commands.Users;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticationResult>
{
    public Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}