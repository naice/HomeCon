using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model
{
    public class Capability
    {
        /// <summary>
        /// Indicates the type of capability, which determines what fields the capability has.
        /// </summary>
        public string Type { get; set; } = "AlexaInterface";
        /// <summary>
        /// The qualified name of the interface that describes the actions for the device.
        /// </summary>
        public string Interface { get; set; }
        /// <summary>
        /// Indicates the interface version that this endpoint supports.
        /// </summary>
        public string Version { get; set; } = "3";
        /// <summary>
        /// <see cref="CapabilityProperties"/>
        /// </summary>
        public CapabilityProperties Properties { get; set; }
        /// <summary>
        /// Indicates whether the properties listed for this endpoint generate Change.Report events.
        /// </summary>
        public bool ProactivelyReported { get; set; }
        /// <summary>
        /// Indicates whether the properties listed for this endpoint can be retrieved for state reporting.
        /// </summary>
        public bool Retrievable { get; set; }
    }

    /// <summary>
    /// Indicates the properties of the interface.
    /// </summary>
    public class CapabilityProperties
    {
        /// <summary>
        /// Indicates the properties of the interface which are supported by this endpoint in the format "name":"<propertyName>". If you do not specify a reportable property of the interface in this array, the default is to assume that proactivelyReported and retrievable for that property are false.
        /// </summary>
        public Dictionary<string,string> Supported { get; set; }
    }
}
