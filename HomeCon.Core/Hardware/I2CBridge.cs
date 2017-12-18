using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Hardware
{
    internal class I2CBridge
    {
        private readonly II2CBus bus;
        private readonly int address;

        public I2CBridge(Configuration config, RestServer.ILogger logger)
        {
            bus = new I2CBus(logger);
            address = config.I2CBridgeAddress;
        }

        public void Execute(II2cDeviceCommand command)
        {
            bus.Execute(address, command.Execute, true);
        }
    }
}
