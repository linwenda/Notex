using Autofac;
using Funzone.BuildingBlocks.Application;
using Funzone.BuildingBlocks.EventBusDapr;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.Services.Albums.Api.Configuration.ExecutionContext;
using Funzone.Services.Albums.Application.IntegrationEvents.EventHandling;
using Funzone.Services.Albums.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Funzone.Services.Albums.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IServiceCollection ServiceCollection { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddDapr();

            services.AddHttpContextAccessor();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddSingleton<IEventBus, DaprEventBus>();
            services.AddSingleton(Log.Logger);
            services.AddTransient<UserRegisteredIntegrationEventHandler>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Funzone.Services.Albums.Api", Version = "v1" });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "http://172.16.100.175:5203";
                options.RequireHttpsMetadata = false;
                options.Audience = "albums-api";
            });

            ServiceCollection = services;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var serviceProvider = ServiceCollection.BuildServiceProvider();
            var connectionString = Configuration.GetConnectionString("SqlServer");
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            var logger = serviceProvider.GetRequiredService<ILogger>();

            builder.RegisterModule(
                new PhotoAlbumModule(
                    connectionString,
                    logger,
                    eventBus));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funzone.Services.Albums.Api v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
