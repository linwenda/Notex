using FluentValidation;
using Notex.Messages.MergeRequests.Commands;

namespace Notex.Core.Validations;

public class UpdateMergeRequestCommandValidator:AbstractValidator<UpdateMergeRequestCommand>
{
    public UpdateMergeRequestCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(128);
        
        RuleFor(v => v.Description)
            .MaximumLength(512);
    }
}