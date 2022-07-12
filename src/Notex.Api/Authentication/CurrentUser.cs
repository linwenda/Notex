using System.Security.Claims;
using Notex.Core.Identity;

namespace Notex.Api.Authentication;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid Id
    {
        get
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.Single(
                    x => x.Type == ClaimTypes.NameIdentifier).Value);
            }

            throw new ApplicationException("HttpContext is not available");
        }
    }
}