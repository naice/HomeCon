using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.RestServer
{
    public class ConverterException : Exception
    {
        public ConverterException(string message) : base (message)
        {

        }

        public ConverterException(Exception inner) : base(inner.Message, inner)
        {

        }
    }
}
