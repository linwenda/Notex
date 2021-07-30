using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Users.Command;
using MediatR;

namespace MarchNote.Api.Configuration.Identity
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
            var response = await _mediator.Send(new AuthenticateCommand(context.UserName, context.Password));

            if (response.Code != DefaultResponseCode.Succeeded)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    response.Message);

                return;
            }

            context.Result = new GrantValidationResult(
                response.Data.Id.ToString(),
                "forms",
                response.Data.Claims);
        }
    }
}