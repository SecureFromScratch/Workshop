using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGreetings
{
    class LogAux
    {
        public static string Scrub(string a_untrustedValue)
        {
            // very naive implementation - do not use it in real life!
            char[] scrubbed = new char[a_untrustedValue.Length];
            int i = 0;
            foreach (char c in a_untrustedValue)
            {
                scrubbed[i++] = char.IsControl(c) ? '?' : c;
            }
            return new string(scrubbed);
        }

        public static string ObscureUsername(string a_username)
        {
            int backslashPos = a_username.IndexOf('\\');
            return (backslashPos == -1) 
                ? Obscure(a_username) 
                : Obscure(a_username.Substring(0, backslashPos)) + '\\' + Obscure(a_username.Substring(backslashPos + 1));
        }

        public static string Obscure(string a_sensitiveValue)
        {
            char[] obscured = new char[Math.Max(a_sensitiveValue.Length, 5)];
            obscured[0] = a_sensitiveValue[0];
            obscured[obscured.Length - 1] = a_sensitiveValue[a_sensitiveValue.Length - 1];
            for (int i = 1; i < obscured.Length - 1; ++i)
            {
                obscured[i] = '*';
            }
            return new string(obscured);
        }

    }
}
