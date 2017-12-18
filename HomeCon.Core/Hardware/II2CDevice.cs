using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Hardware
{
    internal interface II2CDevice
    {
        void Write(byte[] writeBuffer);

        void Read(byte[] readBuffer);

        void WriteRead(byte[] writeBuffer, byte[] readBuffer);
    }
}
