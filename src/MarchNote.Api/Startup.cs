using Autofac;
using MarchNote.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MarchNote.Api.Configuration.Identity;
using MarchNote.Api.Configuration.Swagger;
using MarchNote.Api.Filters;
using MarchNote.Application.Configuration;
using MarchNote.Infrastructure;

namespace MarchNote.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private IServiceCollection ServiceCollection { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => { options.Filters.Add<ExceptionFilter>(); });
            services.AddCustomSwagger();
            services.AddHttpContextAccessor();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddCustomIdentityServer(_configuration);

            ServiceCollection = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarchNote.Api v1"));
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = _configuration.GetConnectionString("SqlServer");
            var attachmentSavePathString = _configuration.GetValue<string>("AttachmentServer:SavePath");
            var serviceProvider = ServiceCollection.BuildServiceProvider();

            builder.RegisterModule(new MarchNoteModule(
                Log.Logger,
                serviceProvider.GetRequiredService<IExecutionContextAccessor>(),
                connectionString,
                attachmentSavePathString));
        }
    }
}