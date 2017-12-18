using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Hardware
{
    internal class SendRC433MhzSignalCommand : II2cDeviceCommand
    {
        private const byte I2C_ACTION_433MHz = 2;

        private string _code = "0000010000";
        private byte _pin;
        private byte _repeats = 10;
        private byte _active = 1;

        public SendRC433MhzSignalCommand WithOnOff(bool onOff)
        {
            if (onOff) _active = 1;
            else _active = 0;
            return this;
        }
        public SendRC433MhzSignalCommand WithCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));
            if (code.Length != 10) throw new FormatException(nameof(code));
            _code = code;
            return this;
        }
        public SendRC433MhzSignalCommand WithPin(byte pin)
        {
            _pin = pin;
            return this;
        }
        public SendRC433MhzSignalCommand WithRepeats(byte count)
        {
            _repeats = count;
            return this;
        }

        public void Execute(II2CDevice i2CDevice)
        {
            i2CDevice.Write(ToPackage());
        }

        private byte[] ToPackage()
        {
            var package = new byte[14];
            package[0] = I2C_ACTION_433MHz;

            // Example package bytes:
            // 1 = CODE_1
            // 2 = CODE_2
            // 3 = CODE_3
            // 4 = CODE_4
            // 5 = CODE_5
            // 6 = CODE_A
            // 7 = CODE_B
            // 8 = CODE_C
            // 9 = CODE_D
            //10 = CODE_E
            //11 = ACTIVE
            //12 = REPEAT_COUNT
            //13 = PIN

            for (int i = 1; i < 11; i++)
            {
                package[i] = (byte)(_code[i-1] == '1' ? 1 : 0);
            }
            package[11] = _active;
            package[12] = _repeats;
            package[13] = _pin;

            return package;
        }
    }
}
