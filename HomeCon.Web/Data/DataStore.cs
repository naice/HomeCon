using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCon.Model;
using HomeCon.Model.Data;
using System.Collections;

namespace HomeCon.Web.Data
{
    public class DataStore : IDataAdapter
    {
        private object dataLock = new object();
        private readonly Dictionary<Type, IList> Data = new Dictionary<Type, IList>();
        private bool _isDataTypesDirty = false;

        public IEnumerable<DataModel> GetData<DataModel>() where DataModel : class, new()
        {
            return GetStorageForType<DataModel>(); 
        }

        IEnumerable<DataModel> GetStorageForType<DataModel>()
        {
            return GetStorageForType(typeof(DataModel)).Cast<DataModel>();
        }
        IList GetStorageForType(Type type)
        {
            if (Data.ContainsKey(type))
            {
                return Data[type];
            }

            var newDataTable = new List<object>();
            Data[type] = newDataTable;
            _isDataTypesDirty = true;
            return newDataTable;
        }
        IList GetStorageCopyForType(Type type)
        {
            return new List<object>(GetStorageForType(type).Cast<object>());
        }
        void UpdateStorageForType(Type type, IList storeage)
        {
            Data[type] = storeage;
        }


        public void Insert(object item, IDataRelation relation)
        {
            var itemType = item.GetType();
            var keyType = relation.GetPrimaryKeyType(item);
            IList storage;
            storage = GetStorageCopyForType(itemType);

            object newKey = null;
            if (keyType == typeof(string))
            {
                newKey = Guid.NewGuid().ToString();
            }
            else
            {
                throw new NotImplementedException($"key generation not implemented for key type {keyType.FullName}");
            }
            relation.SetPrimaryKey(item, newKey);

            storage.Add(item);
            UpdateStorageForType(itemType, storage);
            
            PersistData(itemType);
        }
        public void Update(object item, IDataRelation relation)
        {
            // referencing just persist?
            lock (dataLock)
            {
                PersistData(item.GetType());
            }
        }
        public void Delete(object item, IDataRelation relation)
        {
            var itemType = item.GetType();
            IList storage;
            storage = GetStorageCopyForType(itemType);

            storage.Remove(item);
            UpdateStorageForType(itemType, storage);


            PersistData(itemType);
        }

        private void ObjectToFile(string file, object item)
        {
            System.IO.File.WriteAllText(file, Newtonsoft.Json.JsonConvert.SerializeObject(item));
        }
        private T ObjectFromFile<T>(string file) where T : class, new()
        {
            if (!System.IO.File.Exists(file))
                return null;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(file));
        }
        private object ObjectFromFile(string file, Type type)
        {
            if (!System.IO.File.Exists(file))
                return null;

            return Newtonsoft.Json.JsonConvert.DeserializeObject(System.IO.File.ReadAllText(file), type);
        }


        private void PersistData(Type type)
        {
            if (_isDataTypesDirty)
            {
                ObjectToFile("types.json", Data.Select(data => data.Key.FullName));
                _isDataTypesDirty = false;
            }
            foreach (var data in Data)
            {
                string fname = data.Key.FullName + ".json";
                ObjectToFile(fname, data.Value);
            }
        }
        public void LoadPersistedData()
        {
            LoadData();
        }
        private void LoadData()
        {
            var types = ObjectFromFile<List<string>>("types.json");
            if (types == null) return;

            foreach (var typeName in types)
            {
                var itemType = System.Reflection.Assembly.GetAssembly(typeof(DataStore)).GetType(typeName);
                var listType = typeof(List<>);
                Type[] typeArgs = { itemType };
                var type = listType.MakeGenericType(typeArgs);
                var fileName = typeName + ".json";

                if (!System.IO.File.Exists(fileName))
                    continue;

                var store = ObjectFromFile(typeName + ".json", type);
                UpdateStorageForType(itemType, (IList)store);
            }
        }
    }
}
