using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MediatR;

namespace MarchNote.Application.Users.Command
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