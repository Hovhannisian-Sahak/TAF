namespace TAF.Core.Configuration;

public static class Validations
{
    public static void ValidateTimeouts(TAF.Core.Timeouts.Timeouts timeouts)
    {
        if (timeouts.Default <= 0)
            throw new InvalidOperationException("Timeouts:Default must be greater than 0.");

        if (timeouts.Short <= 0)
            throw new InvalidOperationException("Timeouts:Short must be greater than 0.");

        if (timeouts.Long <= 0)
            throw new InvalidOperationException("Timeouts:Long must be greater than 0.");

        if (timeouts.Retry <= 0)
            throw new InvalidOperationException("Timeouts:Retry must be greater than 0.");

        if (timeouts.Short > timeouts.Long)
            throw new InvalidOperationException("Timeouts:Short cannot be greater than Timeouts:Long.");
    }

    public static void ValidateCredentials(Credentials credentials)
    {
        if (string.IsNullOrWhiteSpace(credentials.Username))
            throw new InvalidOperationException("Credentials:Username is empty.");

        if (string.IsNullOrWhiteSpace(credentials.Password))
            throw new InvalidOperationException("Credentials:Password is empty.");

        if (credentials.Username.Contains("PLACEHOLDER", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Credentials:Username contains placeholder value.");

        if (credentials.Password.Contains("MASKED", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Credentials:Password contains masked placeholder value.");
    }
}
