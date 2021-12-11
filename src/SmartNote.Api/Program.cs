var configuration = GetConfiguration();
Log.Logger = CreateSerilogLogger(configuration);
const string appName = "SmartNote.Api";

try
{

    Log.Information("Configuring web host ({ApplicationContext})...", appName);

    var host = BuildWebHost(configuration, args);

    Log.Information("Starting web host ({ApplicationContext})...", appName);

    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", appName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

Serilog.ILogger CreateSerilogLogger(IConfiguration config)
{
    return new LoggerConfiguration()
        .ReadFrom.Configuration(config)
        .CreateLogger();
}

IWebHost BuildWebHost(IConfiguration config, string[] args) => 
    WebHost.CreateDefaultBuilder(args).CaptureStartupErrors(false)
        .ConfigureAppConfiguration(x => x.AddConfiguration(config))
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseSerilog()
        .Build();


