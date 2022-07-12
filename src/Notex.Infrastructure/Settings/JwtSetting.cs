using Microsoft.Extensions.Configuration;
using Notex.Core.Settings;

namespace Notex.Infrastructure.Settings;

public class JwtSetting : IConfigurationSetting
{
    public JwtSetting(IConfiguration configuration)
    {
        Secret = configuration.GetValue<string>("Authenticate:JwtSecret");
    }

    public string Secret { get; }
}