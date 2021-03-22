using Autofac;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.PhotoAlbums.Application.DomainServices;
using Funzone.PhotoAlbums.Domain.Albums;
using Funzone.PhotoAlbums.Domain.Users;
using Funzone.PhotoAlbums.Infrastructure.Domain.Albums;

namespace Funzone.PhotoAlbums.Infrastructure.Domain
{
    public class DomainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserContext>()
                .As<IUserContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AlbumCounter>()
                .As<IAlbumCounter>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AlbumRepository>()
                .As<IAlbumRepository>()
                .InstancePerLifetimeScope();
        }
    }
}