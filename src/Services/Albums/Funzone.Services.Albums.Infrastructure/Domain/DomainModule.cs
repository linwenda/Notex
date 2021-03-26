using Autofac;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.Services.Albums.Application.DomainServices;
using Funzone.Services.Albums.Domain.PhotoAlbums;
using Funzone.Services.Albums.Domain.Users;
using Funzone.Services.Albums.Infrastructure.Domain.PhotoAlbums;

namespace Funzone.Services.Albums.Infrastructure.Domain
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