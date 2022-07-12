using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Notex.Core.Identity;
using Notex.Core.Identity.Handlers;
using Notex.Infrastructure.Identity;
using Notex.Infrastructure.Settings;

namespace Notex.Api.Authentication;

public static class AuthenticationExtensions
{
    public static void AddCustomAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(new JwtSetting(configuration).Secret);

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityAccessDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme);

            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
        });


        services.AddScoped<ICurrentUser, CurrentUser>();
    }

    public static void AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("UpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
            options.AddPolicy("DeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
        });

        services.AddSingleton<IAuthorizationHandler, SpaceAuthorizationHandler>();
    }
}