using Dapr.Client;
using Funzone.Aggregator.IdentityAccess;
using Funzone.Aggregator.PhotoAlbums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Funzone.Aggregator.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers()
                .AddDapr();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Funzone.Aggregator", Version = "v1"});
            });

            return services;
        }
        
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IIdentityAccessService, IdentityAccessService>(
                _ => new IdentityAccessService(DaprClient.CreateInvokeHttpClient("identityaccessapi")));

            services.AddSingleton<IPhotoAlbumService, PhotoAlbumsService>(
                _ => new PhotoAlbumsService(DaprClient.CreateInvokeHttpClient("photoalbumsapi")));

            return services;
        }
    }
}