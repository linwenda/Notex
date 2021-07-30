using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Users.Command
{
    public class ChangePasswordCommand : ICommand<MarchNoteResponse>
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