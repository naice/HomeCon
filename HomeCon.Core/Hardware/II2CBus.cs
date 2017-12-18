using System;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core.Hardware
{
    internal interface II2CBus
    {
        /// <summary>
        /// Executes the specified action providing the <see cref="II2CDevice"/> for the device with the specified address.
        /// This class is thread safe.
        /// </summary>
        /// <param name="address">The address of the device.</param>
        /// <param name="action">The action which sould be executed. The bus is locked while the action is being executed.</param>
        /// <param name="useCache">Indicates whether the device should be cached internally to improve performance (required if states are polled).</param>
        void Execute(int address, Action<II2CDevice> action, bool useCache = true);
    }
}