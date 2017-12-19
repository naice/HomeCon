using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model
{
    public class Bridge
    {
        /// <summary>
        /// Id
        /// </summary>
        [Data.PrimaryKey]
        public int Id { get; set; }
        /// <summary>
        /// Foreign key for <see cref="Client.Id"/>.
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// IP where to reach the bridge, calls will be send here.
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// The port of the bridge. Default port is 40401.
        /// </summary>
        public int Port { get; set; } = 40401;
        /// <summary>
        /// Freindly name for the bridge. Should be something like bridge cellar, etc...
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Last update of the IP address.
        /// </summary>
        public DateTime LastIPUpdate { get; set; }
    }
}
