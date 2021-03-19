using Autofac;
using Funzone.BuildingBlocks.EventBusDapr;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.IdentityAccess.Api.Configuration.Filters;
using Funzone.IdentityAccess.Api.Configuration.IdentityServer;
using Funzone.IdentityAccess.Api.Users;
using Funzone.IdentityAccess.Infrastructure;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Funzone.IdentityAccess.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IServiceCollection ServiceCollection { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddDapr();
            services.AddSingleton(Log.Logger);
            services.AddSingleton<IEventBus, DaprEventBus>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Funzone.IdentityAccess.Api", Version = "v1" });
            });

            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApis())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddInMemoryPersistedGrants()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.Authority = "http://localhost:5000";
                    x.ApiName = "identityAccessApi";
                    x.RequireHttpsMetadata = false;
                });

            ServiceCollection = services;
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            var serviceProvider = ServiceCollection.BuildServiceProvider();
            var connectionString = Configuration.GetConnectionString("SqlServer");
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();

            containerBuilder.RegisterModule(new IdentityAccessModule(
                connectionString, 
                Log.Logger, 
                eventBus));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funzone.IdentityAccess.Api v1"));
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
