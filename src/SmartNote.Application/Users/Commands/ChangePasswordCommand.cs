using FluentValidation;
using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Users.Commands
{
    public class ChangePasswordCommand : ICommand<Unit>
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
}