using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using TAF.Core.Configuration;

namespace TAF.Core.Logging;

public static class LoggerConfigurator
{
    public static void Configure(LoggingOptions options)
    {
        var config = options ?? new LoggingOptions();
        var repository = (Hierarchy)LogManager.GetRepository(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
        repository.ResetConfiguration();

        var pattern = new PatternLayout("%date %-5level [%thread] %logger - %message%newline");
        pattern.ActivateOptions();

        var consoleAppender = new ConsoleAppender
        {
            Layout = pattern
        };
        consoleAppender.ActivateOptions();

        var logPath = string.IsNullOrWhiteSpace(config.FilePath) ? "logs/taf.log" : config.FilePath;
        var directory = Path.GetDirectoryName(logPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var fileAppender = new RollingFileAppender
        {
            Layout = pattern,
            File = logPath,
            AppendToFile = true,
            RollingStyle = RollingFileAppender.RollingMode.Size,
            MaxSizeRollBackups = 5,
            MaximumFileSize = "10MB",
            StaticLogFileName = true
        };
        fileAppender.ActivateOptions();

        repository.Root.AddAppender(consoleAppender);
        repository.Root.AddAppender(fileAppender);
        repository.Root.Level = ResolveLevel(repository, config.MinLevel);
        repository.Configured = true;
    }

    private static Level ResolveLevel(Hierarchy repository, string? minLevel)
    {
        if (string.IsNullOrWhiteSpace(minLevel))
        {
            return Level.Info;
        }

        var level = repository.LevelMap[minLevel.Trim().ToUpperInvariant()];
        return level ?? Level.Info;
    }
}
