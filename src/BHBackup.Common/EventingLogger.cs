using Microsoft.Extensions.Logging;

namespace BHBackup.Common;

public sealed class EventingLogger : ILogger
{

    public delegate void LogEventHandler(LogLevel logLevel, EventId eventId, Exception? exception, string message);
    public event LogEventHandler? EventLogged;

    #region ILogger Interface

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (logLevel == LogLevel.Information)
        {
            this.EventLogged?.Invoke(
                logLevel, eventId, exception,
                formatter.Invoke(state, exception)
            );
        }
    }

    #endregion

}
