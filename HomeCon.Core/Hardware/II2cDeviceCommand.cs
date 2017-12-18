namespace HomeCon.Core.Hardware
{
    internal interface II2cDeviceCommand
    {
        void Execute(II2CDevice i2CDevice);
    }
}