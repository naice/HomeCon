using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Service
{
    [RestServer.RestServerServiceInstance(RestServer.RestServerServiceInstanceType.Instance)]
    internal class Send433MhzRemoteCodeService : RestServer.RestServerService
    {
        private readonly Hardware.I2CBridge _i2CBridge;
        private readonly Configuration _config;

        public Send433MhzRemoteCodeService(Configuration config, Hardware.I2CBridge i2CBridge)
        {
            _config = config;
            _i2CBridge = i2CBridge;
        }

        [RestServer.RestServerServiceCall("Send433Mhz")]
        public Model.Send433MhzRemoteCodeResponse Send433MhzRemoteCode(Model.Send433MhzRemoteCodeRequest request)
        {
            Model.Send433MhzRemoteCodeResponse model = new Model.Send433MhzRemoteCodeResponse();
            model.Success = false;
            model.Message = "Unknown error.";

            if (request == null || string.IsNullOrEmpty(request.Code) || request.Code.Length != 10)
            {
                model.Message = "No code or invalid size.";
                return model;
            }

            var code = request.Code;
            if (code.Any((c) => c != '0' && c != '1'))
            {
                model.Message = $"Invalid code syntax {request.Code}. Expected 0000000000 - 1111111111 (bit flags) first 5 device group last 5 device id.";
                return model;
            }

            var command = new Hardware.SendRC433MhzSignalCommand()
                .WithCode(code)
                .WithPin(_config.I2CBus433MhzPin)
                .WithRepeats(_config.I2CBus433MhzRepeats)
                .WithOnOff(request.OnOff);
            _i2CBridge.Execute(command);

            model.Success = true;
            return model;
        }
    }
}
