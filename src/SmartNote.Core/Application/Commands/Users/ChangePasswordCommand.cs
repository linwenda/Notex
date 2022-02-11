using FluentValidation;
using MediatR;

namespace SmartNote.Core.Application.Commands.Users;

public class ChangePasswordCommand : IRequest
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(v => v.OldPassword).NotNull().Length(6, 50);
        RuleFor(v => v.NewPassword).NotNull().Length(6, 50);
    }
}