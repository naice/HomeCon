using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HomeCon.Model.Data
{
    public class DataProvider 
    {
        private readonly IDataRelation _dataRelation;
        private readonly IDataAdapter _dataAdapter;
        /// <summary>
        /// Associated data adapter.
        /// </summary>
        protected IDataAdapter Data  => _dataAdapter; 

        public DataProvider(IDataAdapter dataAdapter)
        {
            _dataAdapter = dataAdapter;
            _dataRelation = new DataRelation();
        }



        public Client GetClientById(int clientId)
        {
            return Data.GetClients().Where(client => client.Id == clientId).FirstOrDefault();
        }
        public Client GetClientByEmail(string email)
        {
            return Data.GetClients().Where(client => client.Email == email).FirstOrDefault();
        }

        public Bridge GetBridgeById(int bridgeId)
        {
            return Data.GetBridges().Where(bridge => bridge.Id == bridgeId).FirstOrDefault();
        }
        public IEnumerable<Bridge> GetBridgesByClientId(int clientId)
        {
            return Data.GetBridges().Where(bridge => bridge.ClientId == clientId);
        }

        public DeviceGroup GetDeviceGroupById(int deviceGroupId)
        {
            return Data.GetDeviceGroups().Where(deviceGroup => deviceGroup.Id == deviceGroupId).FirstOrDefault();
        }
        public IEnumerable<DeviceGroup> GetDeviceGroupsByBridgeId(int bridgeId)
        {
            return Data.GetDeviceGroups().Where(deviceGroup => deviceGroup.BridgeId == bridgeId);
        }

        public Device GetDeviceById(string deviceId)
        {
            return Data.GetDevices().Where(device => device.EndPointId == deviceId).FirstOrDefault();
        }
        public IEnumerable<Device> GetDevicesByDeviceGroupId(int deviceGroupId)
        {
            return Data.GetDevices().Where(device => device.DeviceGroupId == deviceGroupId);
        }

        public void Insert(object item)
        {
            Data.Insert(item, _dataRelation);
        }

        public void Delete(object item)
        {
            Data.Delete(item, _dataRelation);
        }

        public void Update(object item)
        {
            Data.Update(item, _dataRelation);
        }
    }
}
