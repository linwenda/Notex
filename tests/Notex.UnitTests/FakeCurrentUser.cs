using System;
using Notex.Core.Configuration;

namespace Notex.UnitTests;

public class FakeCurrentUser : ICurrentUser
{
    public Guid Id => Guid.Parse("d28d21a2-7a13-405d-aba7-9d409456db45");
}