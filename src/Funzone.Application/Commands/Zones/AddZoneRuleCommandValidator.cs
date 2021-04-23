using FluentValidation;

namespace Funzone.Application.Commands.Zones
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