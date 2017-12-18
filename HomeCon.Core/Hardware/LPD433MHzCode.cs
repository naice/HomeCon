using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Hardware
{
    internal class LPD433MHzCode
    {
        public LPD433MHzCode(uint value, byte length, byte repeats)
        {
            Value = value;
            Length = length;
            Repeats = repeats;
        }

        public uint Value { get; }

        public byte Length { get; }

        public byte Repeats { get; }
    }
}
