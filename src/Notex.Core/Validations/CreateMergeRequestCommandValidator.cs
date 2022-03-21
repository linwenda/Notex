using FluentValidation;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Validations;

public class CreateMergeRequestCommandValidator : AbstractValidator<CreateMergeRequestCommand>
{
    public CreateMergeRequestCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(128);
        
        RuleFor(v => v.Description)
            .MaximumLength(512);
    }
}