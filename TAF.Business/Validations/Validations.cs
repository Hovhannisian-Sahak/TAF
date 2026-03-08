namespace TAF.Business.Validations;

public static class Validations
{
    private static readonly log4net.ILog Log = TAF.Core.Logging.AppLogger.For(typeof(Validations));

    public static void ValidatePageState(bool condition, string failureMessage)
    {
        if (!condition)
        {
            Log.Error(failureMessage);
            throw new InvalidOperationException(failureMessage);
        }
    }
}
