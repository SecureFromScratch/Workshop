using System;
using System.Collections.Generic;
using System.Text;

namespace sfslib
{
    public class LoggablePii<TValue> : Pii<TValue>
    {
        private string m_altIdentifier;

        public LoggablePii(TValue a_value, string a_altIdentifier)
            : base(a_value)
        {
            m_altIdentifier = a_altIdentifier;
        }

        public override string ToString() 
        {
            return m_altIdentifier;
        }

        public string ToLoggable()
        {
            return m_altIdentifier;
        }

        public override Pii<TValue> CloneWithTransform(Func<TValue, TValue> a_transform)
        {
            return new LoggablePii<TValue>(a_transform(ExposeUnsecured()), m_altIdentifier);
        }
    }
}
