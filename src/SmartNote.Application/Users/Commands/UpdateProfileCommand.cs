﻿using FluentValidation;
using MediatR;
using SmartNote.Application.Configuration.Commands;

namespace SmartNote.Application.Users.Commands
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