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
        public int Id { get; set; }
        /// <summary>
        /// Name of the client.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email to contact the client.
        /// </summary>
        public string Email { get; set; }
    }
}
