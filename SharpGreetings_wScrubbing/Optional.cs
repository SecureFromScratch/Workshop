using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGreetings
{
    public struct NoOpt
    {
    }

    public struct Optional<T>
    {
        public Optional(NoOpt a_nopopt = new NoOpt())
        {
            m_hasValue = false;
            m_value = default;
        }

        public Optional(T a_value)
        {
            m_hasValue = true;
            m_value = a_value;
        }

        public bool HasValue
        {
            get { return m_hasValue; }
        }

        public T Value
        {
            get
            {
                if (!m_hasValue)
                {
                    throw new InvalidOperationException("Cannot ask for value of an empty optional");
                }
                return m_value;
            }
        }

        public static implicit operator bool(Optional<T> a_optional)
        {
            return a_optional.HasValue;
        }

        private bool m_hasValue;
        private T m_value;
    }
}
