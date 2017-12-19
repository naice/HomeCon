using System;
using System.Collections.Generic;
using System.Text;
using DPC = HomeCon.Model.DisplayCategories;

namespace HomeCon.Model.DeviceDefaults
{
    public class DeviceSimpleSwitch : Device
    {
        public DeviceSimpleSwitch()
        {
            DisplayCategories = new List<string>()
            {
                DPC.SWITCH,
            };

            Capabilities = new List<Capability>()
            {
                new Capability()
                {
                    Interface = "Alexa",
                },
                new Capability()
                {
                    Interface = "Alexa.PowerController",
                    Properties = new CapabilityProperties()
                    {
                        Supported = new Dictionary<string, string>()
                        {
                            { "name", "powerState" },
                        }
                    },
                    ProactivelyReported = true,
                    Retrievable = true,
                },
                new Capability()
                {
                    Interface = "Alexa.EndpointHealth",
                    Properties = new CapabilityProperties()
                    {
                        Supported = new Dictionary<string, string>()
                        {
                            { "name", "connectivity" },
                        }
                    },
                    ProactivelyReported = true,
                    Retrievable = true,
                },
            };
        }
    }
}
