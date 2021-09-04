using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Users.Command
{
    public class UpdateProfileCommand : ICommand<MarchNoteResponse>
    {
        public string NickName { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
    }

    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(v => v.NickName).MaximumLength(32);
            RuleFor(v => v.Bio).MaximumLength(128);
            RuleFor(v => v.Avatar).MaximumLength(512);
        }
    }
}