using System;
using Microsoft.Extensions.Configuration;
using DotNetEnv;
using TAF.Core.Configuration;
using TAF.Core.Logging;
using TAF.Core.WebDriver;
using TAF.Core.Timeouts;
using TimeoutSettings = TAF.Core.Timeouts.Timeouts;
namespace TAF.Core.Configuration;

public static class Configuration
{
    static Configuration() => Init();

    public static BrowserType BrowserType { get; private set; }
    public static string AppUrl { get; private set; } = string.Empty;
    public static TimeoutSettings Timeouts { get; private set; } = new();
    public static Credentials Credentials { get; private set; } = new();
    public static BrowserOptions BrowserOptions { get; private set; } = new();
    public static LoggingOptions Logging { get; private set; } = new();

    private static void Init()
    {
        // Load .env file first
        Env.Load();  // looks for .env in project root by default

        var environment = Environment.GetEnvironmentVariable("TAF_ENV") ?? "dev";

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // -------- BrowserType --------
        var browserString = config["BrowserType"] ?? "Chrome";
        if (!Enum.TryParse(browserString, true, out BrowserType browser))
            throw new InvalidOperationException($"Invalid BrowserType value: {browserString}");
        BrowserType = browser;

        // -------- App URL --------
        AppUrl = Environment.GetEnvironmentVariable("APP_URL") 
                 ?? config["ApplicationUrl"] 
                 ?? throw new InvalidOperationException("ApplicationUrl is missing.");

        // -------- Strongly Typed Sections --------
        config.GetSection("Timeouts").Bind(Timeouts);
        config.GetSection("BrowserOptions").Bind(BrowserOptions);
        config.GetSection("Logging").Bind(Logging);

        LoggerConfigurator.Configure(Logging);

        // -------- Credentials from .env or JSON fallback --------
        Credentials.Username = Environment.GetEnvironmentVariable("TAF_USERNAME") 
                               ?? config["Credentials:Username"] 
                               ?? throw new InvalidOperationException("Username is missing.");

        Credentials.Password = Environment.GetEnvironmentVariable("TAF_PASSWORD") 
                               ?? config["Credentials:Password"] 
                               ?? throw new InvalidOperationException("Password is missing.");

        // -------- Validation --------
        // Validations.ValidateTimeouts(Timeouts);
        // Validations.ValidateCredentials(Credentials);
    }
}
