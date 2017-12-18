using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Service
{
    [RestServer.RestServerServiceInstance(RestServer.RestServerServiceInstanceType.SingletonLazy)]
    internal class IFTTTService : RestServer.RestServerService
    {
        private readonly Hardware.I2CBridge _i2CBridge;
        private readonly Configuration _config;
        private readonly Dictionary<string, bool> _states;

        public IFTTTService(Configuration config, Hardware.I2CBridge i2CBridge)
        {
            _config = config;
            _i2CBridge = i2CBridge;
            _states = new Dictionary<string, bool>();
        }

        private bool Toggle(string key)
        {
            var b = true;

            if (!_states.ContainsKey(key))
            {
                b = _states[key] =  true;
            }
            else
            {
                b = _states[key] = !_states[key];
            }

            return b;
        }

        [RestServer.RestServerServiceCall("IFTTTSwitch")]
        public void Send433MhzRemoteCode(Model.IFTTTRequest request)
        {
            var code = request?.Group + request?.Device;
            if (string.IsNullOrEmpty(code))
                return;

            var command = new Hardware.SendRC433MhzSignalCommand()
                .WithCode(code)
                .WithPin(_config.I2CBus433MhzPin)
                .WithRepeats(_config.I2CBus433MhzRepeats)
                .WithOnOff(Toggle(code));
            _i2CBridge.Execute(command);
        }
    }
}
