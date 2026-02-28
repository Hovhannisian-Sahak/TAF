namespace TAF.Core.Configuration;

public class LoggingOptions
{
    public string MinLevel { get; set; } = "INFO";
    public string FilePath { get; set; } = "logs/taf.log";
}
