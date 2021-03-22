using System.Reflection;
using Funzone.PhotoAlbums.Application.Commands.CreateAlbum;

namespace Funzone.PhotoAlbums.Infrastructure
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CreateAlbumCommand).Assembly;
    }
}