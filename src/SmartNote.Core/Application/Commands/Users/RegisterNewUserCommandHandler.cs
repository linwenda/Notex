using MediatR;

namespace SmartNote.Core.Application.Commands.Users;

public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand>
{
    public Task<Unit> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}