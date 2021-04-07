using Autofac;
using Funzone.BuildingBlocks.Infrastructure;
using Funzone.Services.Albums.Application.DomainServices;
using Funzone.Services.Albums.Domain.Albums;
using Funzone.Services.Albums.Domain.Pictures;
using Funzone.Services.Albums.Domain.Users;
using Funzone.Services.Albums.Infrastructure.Domain.Albums;
using Funzone.Services.Albums.Infrastructure.Domain.Pictures;

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

            builder.RegisterType<PictureRepository>()
                .As<IPictureRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PictureCounter>()
                .As<IPictureCounter>()
                .InstancePerLifetimeScope();
        }
    }
}