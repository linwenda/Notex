using System.Reflection;
using Funzone.IdentityAccess.Application.Users.RegisterUser;

namespace Funzone.IdentityAccess.Infrastructure
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(RegisterUserWithEmailCommand).Assembly;
    }
}