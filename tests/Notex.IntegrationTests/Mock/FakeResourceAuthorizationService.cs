using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Notex.Core.Authorization;

namespace Notex.IntegrationTests.Mock;

public class FakeResourceAuthorizationService : IResourceAuthorizationService
{
    public Task CheckAsync(object resource, IAuthorizationRequirement requirement)
    {
        return Task.CompletedTask;
    }
}