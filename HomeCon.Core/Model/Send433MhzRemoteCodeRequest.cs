using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Model
{
    class Send433MhzRemoteCodeRequest
    {
        public bool OnOff { get; set; }
        public string Code { get; set; }
    }
}
