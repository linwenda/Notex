using FluentValidation;
using MediatR;

namespace SmartNote.Core.Application.Users.Contracts
{
    public class UpdateProfileCommand : ICommand<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
    }

    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(v => v.LastName).MaximumLength(32);
            RuleFor(v => v.FirstName).MaximumLength(32);
            RuleFor(v => v.Bio).MaximumLength(128);
            RuleFor(v => v.Avatar).MaximumLength(512);
        }
    }
}