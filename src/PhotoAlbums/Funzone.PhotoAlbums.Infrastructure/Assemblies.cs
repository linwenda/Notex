using Funzone.PhotoAlbums.Application.Albums.CreateAlbum;
using System.Reflection;

namespace Funzone.PhotoAlbums.Infrastructure
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CreateAlbumCommand).Assembly;
    }
}