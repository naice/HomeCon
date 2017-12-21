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



        public Client GetClientById(string clientId)
        {
            return Data.GetData<Client>().Where(client => client.Id == clientId).FirstOrDefault();
        }
        public Client GetClientByEmail(string email)
        {
            return Data.GetData<Client>().Where(client => client.Email == email).FirstOrDefault();
        }
        public Client GetClientByEmailNormalized(string emailNormalized)
        {
            return Data.GetData<Client>().Where(client => client.EmailNormalized == emailNormalized).FirstOrDefault();
        }

        public Bridge GetBridgeById(string bridgeId)
        {
            return Data.GetData<Bridge>().Where(bridge => bridge.Id == bridgeId).FirstOrDefault();
        }
        public IEnumerable<Bridge> GetBridgesByClientId(string clientId)
        {
            return Data.GetData<Bridge>().Where(bridge => bridge.ClientId == clientId);
        }

        public DeviceGroup GetDeviceGroupById(string deviceGroupId)
        {
            return Data.GetData<DeviceGroup>().Where(deviceGroup => deviceGroup.Id == deviceGroupId).FirstOrDefault();
        }
        public IEnumerable<DeviceGroup> GetDeviceGroupsByBridgeId(string bridgeId)
        {
            return Data.GetData<DeviceGroup>().Where(deviceGroup => deviceGroup.BridgeId == bridgeId);
        }

        public Device GetDeviceById(string deviceId)
        {
            return Data.GetData<Device>().Where(device => device.EndPointId == deviceId).FirstOrDefault();
        }
        public IEnumerable<Device> GetDevicesByDeviceGroupId(string deviceGroupId)
        {
            return Data.GetData<Device>().Where(device => device.DeviceGroupId == deviceGroupId);
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
