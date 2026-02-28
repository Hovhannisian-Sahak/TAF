namespace TAF.Business.Validations;

public static class Validations
{
    public static void ValidatePageState(bool condition, string failureMessage)
    {
        if (!condition)
        {
            throw new InvalidOperationException(failureMessage);
        }
    }
}
