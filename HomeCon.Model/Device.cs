using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model
{
    public class Device
    {
        /// <summary>
        /// A device identifier. The identifier must be unique across all devices owned by an end user within the domain for the skill. In addition, the identifier needs to be consistent across multiple discovery requests for the same device. An identifier can contain any letter or number and the following special characters: _ - = # ; : ? @ &. The identifier cannot exceed 256 characters.	String that contains any letter or number and the following special characters: _ - = # ; : ? @ 
        /// </summary>
        public string EndPointId { get; set; }
        /// <summary>
        /// The name of the device manufacturer. This value cannot exceed 128 characters.
        /// </summary>
        public string ManufracturerName { get; set; }
        /// <summary>
        /// The name used by the customer to identify the device. This value cannot exceed 128 characters and should not contain special characters or punctuation.	
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// A human-readable description of the device. This value cannot exceed 128 characters. The description should contain the manufacturer name or how the device is connected. For example, "Smart Lock by Sample Manufacturer" or "WiFi Thermostat connected via SmartHub". This value cannot exceed 128 characters.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Indicates the group name where the device should display in the Alexa app. See Display categories for supported values.
        /// </summary>
        public List<string> DisplayCategories { get; set; }
        /// <summary>
        /// String name/value pairs that provide additional information about a device for use by the skill. The contents of this property cannot exceed 5000 bytes. The API doesn't use or understand this data.
        /// </summary>
        public Dictionary<string,string> Cookie { get; set; }
        /// <summary>
        /// An array of capability objects that represents actions particular device supports and can respond to. A capability object can contain different fields depending on the type.
        /// </summary>
        public List<string> Capabilities { get; set; }
    }
}
