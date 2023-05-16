using sfslib;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGreetings
{
    class AutoScrubLogger<T>
    {
        private ILogger<T> m_origLogger;

        public AutoScrubLogger(ILogger<T> a_origLogger)
        {
            m_origLogger = a_origLogger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return m_origLogger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return m_origLogger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(FormattableString a_formattable)
        {
            m_origLogger.LogDebug(a_formattable.ToString(new Prov()));
        }
        public void LogInformation(FormattableString a_formattable)
        {
            m_origLogger.LogInformation(a_formattable.ToString(new Prov()));
        }
        public void LogWarning(FormattableString a_formattable)
        {
            m_origLogger.LogWarning(a_formattable.ToString(new Prov()));
        }
        public void LogError(FormattableString a_formattable)
        {
            m_origLogger.LogError(a_formattable.ToString(new Prov()));
        }
        public void LogCritical(FormattableString a_formattable)
        {
            m_origLogger.LogCritical(a_formattable.ToString(new Prov()));
        }

        private class Prov : IFormatProvider
        {
            public object GetFormat(Type formatType)
            {
                return new Formatter();
            }
        }

        class Formatter : ICustomFormatter
        {
            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                if (arg is Pii<string>)
                {
                    return LogAux.Obscure((arg as Pii<string>).ExposeUnsecured());
                }
                return LogAux.Scrub(arg.ToString());
            }
        }
    }
}
