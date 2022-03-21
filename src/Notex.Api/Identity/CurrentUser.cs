using Notex.Core.Configuration;

namespace Notex.Api.Identity;

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
                    x => x.Type == "sub").Value);
            }

            throw new ApplicationException("HttpContext is not available");
        }
    }
}