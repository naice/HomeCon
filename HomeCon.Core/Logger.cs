using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core
{
    internal class Log
    {
        public static RestServer.ILogger DefaultLogger { get; set; } = new DefaultLoggerImplementation();

#pragma warning disable IDE1006
        internal static void i(string msg, params object[] args)
        {
            if (DefaultLogger == null) return;

            WriteLine("INFO      " + string.Format(msg, args));
        }
        internal static void e(string msg, params object[] args)
        {
            if (DefaultLogger == null) return;

            WriteLine("ERROR     " + string.Format(msg, args));
        }
        internal static void w(string msg, params object[] args)
        {
            if (DefaultLogger == null) return;

            WriteLine("WARNING   " + string.Format(msg, args));
        }
#pragma warning restore IDE1006 

        private static void WriteLine(string msg)
        {
            DefaultLogger.WriteLine(msg);
        }

        private class DefaultLoggerImplementation : RestServer.ILogger
        {
            public void Write(string message)
            {
                System.Diagnostics.Debug.Write(message);
            }

            public void WriteLine(string message)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }
        }
    }
}
