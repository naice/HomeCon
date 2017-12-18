using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCon.RestServer;
using System.Reflection;
using Windows.Networking.Connectivity;
using Windows.Networking;
using System.Net;

namespace HomeCon.Core
{
    class ApiServer
    {
        private readonly RestServer.RestServer _restServer;
        private readonly ILogger _logger;
        private readonly IConverter _converter;
        private readonly Configuration _config;

        public ApiServer(Configuration config, IConverter converter, ILogger logger, IRestServerServiceDependencyResolver resolver)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restServer = new RestServer.RestServer(
                endPoint:                       GetIPEndPoint(config.ApiServerPort), 
                restServerDependencyResolver:   resolver, 
                converter:                      converter, 
                assemblys:                      typeof(ApiServer).GetTypeInfo().Assembly);

            Log.i($"Starting: IP:{_restServer.IPEndPoint.Address.ToString()} Port:{_restServer.IPEndPoint.Port}");
            _restServer.Start();
        }

        private static IPEndPoint GetIPEndPoint(int port)
        {
            var result = new IPEndPoint(IPAddress.Loopback, port);
            var icp = NetworkInformation.GetInternetConnectionProfile();

            if (icp?.NetworkAdapter == null) return null;
            var hostname =
                NetworkInformation.GetHostNames()
                    .FirstOrDefault(
                        hn =>
                            hn.Type == HostNameType.Ipv4 &&
                            hn.IPInformation?.NetworkAdapter != null &&
                            hn.IPInformation.NetworkAdapter.NetworkAdapterId == icp.NetworkAdapter.NetworkAdapterId);

            if (hostname != null)
            {
                if (IPAddress.TryParse(hostname.CanonicalName, out IPAddress addr))
                    result = new IPEndPoint(addr, port);
            }

            // the ip address
            return result;
        }
    }
}
