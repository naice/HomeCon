using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model.Data
{
    public interface IDataAdapter
    {
        IEnumerable<Bridge> GetBridges();
        IEnumerable<Client> GetClients();
        IEnumerable<Device> GetDevices();
        IEnumerable<DeviceGroup> GetDeviceGroups();

        void Insert(object item, IDataRelation relation);
        void Delete(object item, IDataRelation relation);
        void Update(object item, IDataRelation relation);
    }
}
