using System;
using Notex.Core.Configuration;

namespace Notex.IntegrationTests.Mock;

public class FakeCurrentUser : ICurrentUser
{
    public Guid Id => Guid.Parse("9e3163b9-1ae6-4652-9dc6-7898ab7b7a00");
}