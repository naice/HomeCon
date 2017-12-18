using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Model
{
    internal class IFTTTRequest 
    {
        public string Group { get; set; }
        public string Device { get; set; }
        public bool? OnOff { get; set; }
    }
}
