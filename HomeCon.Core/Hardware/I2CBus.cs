using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace HomeCon.Core.Hardware
{
    internal class I2CBus : II2CBus
    {
        private readonly Dictionary<int, I2cDevice> _deviceCache = new Dictionary<int, I2cDevice>();

        private readonly string _i2CBusId;

        private readonly RestServer.ILogger _logger;
        private readonly object _syncRoot = new object();

        public I2CBus(RestServer.ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            string deviceSelector = I2cDevice.GetDeviceSelector();

            DeviceInformationCollection deviceInformation = DeviceInformation.FindAllAsync(deviceSelector).AsTask().Result;
            if (deviceInformation.Count == 0)
            {
                throw new InvalidOperationException("I2C bus not found.");
            }
            _i2CBusId = deviceInformation.First().Id;
        }

        public void Execute(int address, Action<II2CDevice> action, bool useCache = true)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (_syncRoot)
            {
                I2cDevice device = null;
                try
                {
                    device = GetI2CDevice(address, useCache);
                    action(new I2CDeviceAdapter(device));
                }
                catch (Exception exception)
                {
                    // Ensure that the application will not crash if some devices are currently not available etc.
                    _logger.WriteLine("Error while accessing I2C device with address " + address + ". " + exception.Message);
                }
                finally
                {
                    if (device != null && !useCache)
                    {
                        device.Dispose();
                    }
                }
            }
        }

        public static IEnumerable<byte> FindDevices()
        {
            // *** 
            // *** Get a selector string that will return all I2C controllers on the system 
            // *** 
            string aqs = I2cDevice.GetDeviceSelector();
            // *** 
            // *** Find the I2C bus controller device with our selector string 
            // *** 
            var dis = DeviceInformation.FindAllAsync(aqs).AsTask().Result;
            if (dis.Count > 0)
            {
                const int minimumAddress = 8;
                const int maximumAddress = 77;
                for (byte address = minimumAddress; address <= maximumAddress; address++)
                {
                    var settings = new I2cConnectionSettings(address);
                    settings.BusSpeed = I2cBusSpeed.FastMode;
                    settings.SharingMode = I2cSharingMode.Shared;
                    // *** 
                    // *** Create an I2cDevice with our selected bus controller and I2C settings 
                    // *** 
                    using (I2cDevice device = I2cDevice.FromIdAsync(dis[0].Id, settings).AsTask().Result)
                    {
                        if (device != null)
                        {
                            bool written = false;
                            try
                            {
                                byte[] writeBuffer = new byte[33];
                                device.Write(writeBuffer);
                                // *** 
                                // *** If no exception is thrown, there is 
                                // *** a device at this address. 
                                // *** 
                                written = true;
                            }
                            catch
                            {
                                // *** 
                                // *** If the address is invalid, an exception will be thrown. 
                                // *** 
                            }
                            if (written)
                                yield return address;
                        }
                    }
                }
            }
        }



        private I2cDevice GetI2CDevice(int address, bool useCache)
        {
            // TODO: The cache is required because using the I2cDevice.FromIdAsync method every time tooks a very long time.
            // Polling the inputs can take up to 300ms (for all) which is too slow (some very short pressed buttons are missed).
            // The Arduino Nano T&H bridge does not work correctly when reusing the device. More investigation is required!
            // At this time, the cache can be disabled for certain devices.
            I2cDevice device;
            if (!useCache || !_deviceCache.TryGetValue(address, out device))
            {
                var settings = new I2cConnectionSettings(address);
                settings.BusSpeed = I2cBusSpeed.StandardMode;
                settings.SharingMode = I2cSharingMode.Exclusive;

                device = I2cDevice.FromIdAsync(_i2CBusId, settings).AsTask().Result;

                if (useCache)
                {
                    _deviceCache.Add(address, device);
                }
            }

            return device;
        }
    }
}