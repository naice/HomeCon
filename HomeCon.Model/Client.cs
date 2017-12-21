using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model
{
    public class Client
    {
        /// <summary>
        /// Id
        /// </summary>
        [Data.PrimaryKey]
        public string Id { get; set; }
        /// <summary>
        /// Name of the client.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email to contact the client.
        /// </summary>
        public string Email { get; set; }
        public string EmailNormalized { get; set; }


        // ASP STUFF
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public int AccessFailedCount { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
    }
}
