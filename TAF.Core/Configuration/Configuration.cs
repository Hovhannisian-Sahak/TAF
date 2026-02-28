using DotNetEnv;
using Microsoft.Extensions.Configuration;
using TAF.Core.Logging;
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
        var configBasePath = ResolveConfigurationBasePath();
        LoadEnvironmentVariables(configBasePath);
        var environment = Environment.GetEnvironmentVariable("TAF_ENV") ?? "dev";

        var config = new ConfigurationBuilder()
            .SetBasePath(configBasePath)
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

        // -------- Credentials from .env only --------
        Credentials.Username = Environment.GetEnvironmentVariable("TAF_USERNAME")
                               ?? throw new InvalidOperationException("username is missing");

        Credentials.Password = Environment.GetEnvironmentVariable("TAF_PASSWORD")
                               ?? throw new InvalidOperationException("password is missing");

        // -------- Validation --------
        Validations.ValidateTimeouts(Timeouts);
        Validations.ValidateCredentials(Credentials);
    }

    private static string ResolveConfigurationBasePath()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var fromCurrent = FindDirectoryContainingAppSettings(currentDir);
        if (fromCurrent != null)
            return fromCurrent;

        var baseDir = AppContext.BaseDirectory;
        var fromBaseDir = FindDirectoryContainingAppSettings(baseDir);
        if (fromBaseDir != null)
            return fromBaseDir;

        throw new FileNotFoundException(
            $"Could not find appsettings.json starting from '{currentDir}' or '{baseDir}'.");
    }

    private static string? FindDirectoryContainingAppSettings(string startDirectory)
    {
        var directory = new DirectoryInfo(startDirectory);
        while (directory != null)
        {
            var candidate = Path.Combine(directory.FullName, "appsettings.json");
            if (File.Exists(candidate))
                return directory.FullName;

            directory = directory.Parent;
        }

        return null;
    }

    private static void LoadEnvironmentVariables(string configBasePath)
    {
        var envPath = Path.Combine(configBasePath, ".env");
        if (!File.Exists(envPath))
            return;

        Env.Load(envPath);
    }
}
