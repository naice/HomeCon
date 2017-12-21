using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model
{
    public class DeviceGroup
    {
        /// <summary>
        /// Id
        /// </summary>
        [Data.PrimaryKey]
        public string Id { get; set; }

        /// <summary>
        /// Foreign key for <see cref="Bridge.Id"/>
        /// </summary>
        public string BridgeId { get; set; }

        /// <summary>
        /// The group name i.e. Wohnzimmer
        /// </summary>
        public string Name { get; set; }
        
    }
}
