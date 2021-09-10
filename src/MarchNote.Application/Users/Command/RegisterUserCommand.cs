﻿using System;
using FluentValidation;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Responses;

namespace MarchNote.Application.Users.Command
{
    public class RegisterUserCommand : ICommand<MarchNoteResponse<Guid>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(v => v.LastName).MaximumLength(32);
            RuleFor(v => v.FirstName).MaximumLength(32);

            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(64);
        }
    }
}