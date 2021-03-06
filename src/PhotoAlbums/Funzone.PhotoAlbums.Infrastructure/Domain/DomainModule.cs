using Autofac;
using Funzone.PhotoAlbums.Application.Albums;
using Funzone.PhotoAlbums.Domain.Albums;

namespace Funzone.PhotoAlbums.Infrastructure.Domain
{
    public class DomainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AlbumCounter>()
                .As<IAlbumCounter>()
                .InstancePerLifetimeScope();
        }
    }
}