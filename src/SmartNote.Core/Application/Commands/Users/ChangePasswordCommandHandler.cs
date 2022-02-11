using MediatR;

namespace SmartNote.Core.Application.Commands.Users;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    public Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}