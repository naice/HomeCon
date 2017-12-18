using System;
using Windows.Devices.I2c;

namespace HomeCon.Core.Hardware
{
    internal class I2CDeviceAdapter : II2CDevice
    {
        private readonly I2cDevice _i2CDevice;

        public I2CDeviceAdapter(I2cDevice i2cDevice)
        {
            _i2CDevice = i2cDevice ?? throw new ArgumentNullException(nameof(i2cDevice));
        }

        public void Write(byte[] writeBuffer)
        {
            if (writeBuffer == null) throw new ArgumentNullException(nameof(writeBuffer));

            _i2CDevice.Write(writeBuffer);
        }

        public void Read(byte[] readBuffer)
        {
            if (readBuffer == null) throw new ArgumentNullException(nameof(readBuffer));

            _i2CDevice.Read(readBuffer);
        }

        public void WriteRead(byte[] writeBuffer, byte[] readBuffer)
        {
            if (readBuffer == null) throw new ArgumentNullException(nameof(readBuffer));
            if (writeBuffer == null) throw new ArgumentNullException(nameof(writeBuffer));

            _i2CDevice.WriteRead(writeBuffer, readBuffer);
        }
    }
}