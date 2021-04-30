using FluentValidation;

namespace Funzone.Application.ZoneRules.Commands
{
    public class AddZoneRuleCommandValidator : AbstractValidator<AddZoneRuleCommand>
    {
        public AddZoneRuleCommandValidator()
        {
            RuleFor(v => v.Title).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(v => v.Description).MaximumLength(128);
        }
    }
}