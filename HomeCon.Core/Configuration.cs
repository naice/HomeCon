using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core
{
    class Configuration
    {
        public int ApiServerPort { get; set; } = 40401;

        // I2C Setup
        public int I2CBridgeAddress { get; set; } = 0x40;
        // I2C BUS Setup
        public byte I2CBus433MhzRepeats { get; set; } = 10;
        public byte I2CBus433MhzPin { get; set; } = A0;

        // Arduino Leonardo Mapping of analog pins as digital I/O 
        const byte A0 = 18;
        const byte A1 = 19;
        const byte A2 = 20;
        const byte A3 = 21;
        const byte A4 = 22;
        const byte A5 = 23;
        const byte A6 = 24; // D4
        const byte A7 = 25; // D6
        const byte A8 = 26; // D8
        const byte A9 = 27; // D9
        const byte A10 = 28; // D10
        const byte A11 = 29; // D12
    }
}
