using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace HomeCon.Model.Data
{
    class DataRelation : IDataRelation
    {
        class RelationInformation
        {
            public Func<object, object> Getter { get; set; }
            public Action<object, object> Setter { get; set; }
            public Type KeyType { get; set; }
        }

        private readonly Dictionary<Type, RelationInformation> _typeToRelationInformationMap = new Dictionary<Type, RelationInformation>();

        private RelationInformation GetRelationInformation(Type itemType)
        {
            if (_typeToRelationInformationMap.ContainsKey(itemType))
                return _typeToRelationInformationMap[itemType];

            var primaryKeyAttributeType = typeof(PrimaryKeyAttribute);
            var pkProperty = itemType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(property => property.GetCustomAttribute(primaryKeyAttributeType) != null).FirstOrDefault();

            if (pkProperty == null)
                throw new InvalidOperationException($"The given type {itemType.FullName} has no {nameof(PrimaryKeyAttribute)} property.");
            if (!pkProperty.CanRead || !pkProperty.CanWrite)
                throw new InvalidOperationException($"The primary key for {itemType.FullName} ({pkProperty.Name}) must be readable and writeable.");

            return _typeToRelationInformationMap[itemType] = new RelationInformation() {
                Getter = new Func<object, object>((obj) => pkProperty.GetValue(obj)),
                Setter = new Action<object, object>((obj, value) => pkProperty.SetValue(obj, value)),
                KeyType = pkProperty.PropertyType,
            };               
        }

        public object GetPrimaryKey(object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var relationInformation = GetRelationInformation(item.GetType());
            return relationInformation.Getter(item);
        }

        public Type GetPrimaryKeyType(object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var relationInformation = GetRelationInformation(item.GetType());
            return relationInformation.KeyType;
        }

        public void SetPrimaryKey(object item, object key)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var relationInformation = GetRelationInformation(item.GetType());
            relationInformation.Setter(item, key);
        }
    }
}
