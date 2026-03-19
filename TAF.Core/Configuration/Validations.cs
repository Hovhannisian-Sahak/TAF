namespace TAF.Core.Configuration;

public static class Validations
{
    private static readonly log4net.ILog Log = TAF.Core.Logging.AppLogger.For(typeof(Validations));

    public static void ValidateTimeouts(TAF.Core.Timeouts.Timeouts timeouts)
    {
        if (timeouts.Default <= 0)
        {
            const string message = "Timeouts:Default must be greater than 0.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }

        if (timeouts.Short <= 0)
        {
            const string message = "Timeouts:Short must be greater than 0.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }

        if (timeouts.Long <= 0)
        {
            const string message = "Timeouts:Long must be greater than 0.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }

        if (timeouts.Retry <= 0)
        {
            const string message = "Timeouts:Retry must be greater than 0.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }

        if (timeouts.Short > timeouts.Long)
        {
            const string message = "Timeouts:Short cannot be greater than Timeouts:Long.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }
    }

    public static void ValidateCredentials(Credentials credentials)
    {
        if (string.IsNullOrWhiteSpace(credentials.Username))
        {
            const string message = "Credentials:Username is empty.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }

        if (string.IsNullOrWhiteSpace(credentials.Password))
        {
            const string message = "Credentials:Password is empty.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }

        if (credentials.Username.Contains("PLACEHOLDER", StringComparison.OrdinalIgnoreCase))
        {
            const string message = "Credentials:Username contains placeholder value.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }

        if (credentials.Password.Contains("MASKED", StringComparison.OrdinalIgnoreCase))
        {
            const string message = "Credentials:Password contains masked placeholder value.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }
    }

    public static void ValidateApiOptions(ApiOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.BaseUrl))
        {
            const string message = "Api:BaseUrl is missing.";
            Log.Error(message);
            throw new InvalidOperationException(message);
        }
    }
}
