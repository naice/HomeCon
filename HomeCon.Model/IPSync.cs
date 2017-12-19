using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model
{
    public class IPSync
    {
        /// <summary>
        /// Key for <see cref="Client.Id"/>
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Only sync defined bridges, null sync for all.
        /// </summary>
        public int[] Bridges { get; set; }
    }
}
