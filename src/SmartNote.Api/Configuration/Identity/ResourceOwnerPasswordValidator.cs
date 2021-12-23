using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;
using SmartNote.Core.Application.Users.Commands;
using SmartNote.Core.Domain.Users.Exceptions;

namespace SmartNote.Api.Configuration.Identity
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IMediator _mediator;

        public ResourceOwnerPasswordValidator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var response = await _mediator.Send(new AuthenticateCommand(context.UserName, context.Password));
                context.Result = new GrantValidationResult(
                    response.Id.ToString(),
                    "forms",
                    response.Claims);
            }
            catch (IncorrectEmailOrPasswordException ex)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    ex.Message);
            }
        }
    }
}