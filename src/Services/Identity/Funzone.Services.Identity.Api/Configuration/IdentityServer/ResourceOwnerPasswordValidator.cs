using System.Threading.Tasks;
using Funzone.Services.Identity.Application.Commands.Authenticate;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MediatR;

namespace Funzone.Services.Identity.Api.Configuration.IdentityServer
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
            var result = await _mediator.Send(new AuthenticateCommand(context.UserName, context.Password));

            if (!result.IsAuthenticated)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    result.AuthenticationError);

                return;
            }

            context.Result = new GrantValidationResult(
                result.User.Id.ToString(),
                "forms",
                result.User.Claims);
        }
    }
}