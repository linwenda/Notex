using System.Reflection;
using Funzone.Services.Albums.Application.Commands.CreateAlbum;

namespace Funzone.Services.Albums.Infrastructure
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CreateAlbumCommand).Assembly;
    }
}