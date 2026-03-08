using log4net;

namespace TAF.Core.Logging;

public static class AppLogger
{
    public static ILog For<T>() => LogManager.GetLogger(typeof(T));

    public static ILog For(Type type) => LogManager.GetLogger(type);
}
