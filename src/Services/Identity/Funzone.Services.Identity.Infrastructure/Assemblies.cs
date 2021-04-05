using System.Reflection;
using Funzone.Services.Identity.Application.Commands.RegisterUser;

namespace Funzone.Services.Identity.Infrastructure
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(RegisterUserByEmailCommand).Assembly;
    }
}