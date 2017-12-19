using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace HomeCon.Model.Data
{
    class DataRelation : IDataRelation
    {
        private readonly Dictionary<Type, Func<object, object>> _typeToPrimaryKeyGetterMap = new Dictionary<Type, Func<object, object>>();

        private Func<object, object> GetPrimaryKeyGetter(Type itemType)
        {
            if (_typeToPrimaryKeyGetterMap.ContainsKey(itemType))
                return _typeToPrimaryKeyGetterMap[itemType];

            var primaryKeyAttributeType = typeof(PrimaryKeyAttribute);
            var pkProperty = itemType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(property => property.GetCustomAttribute(primaryKeyAttributeType) != null).FirstOrDefault();

            if (pkProperty == null)
                throw new InvalidOperationException($"The given type {itemType.FullName} has no {nameof(PrimaryKeyAttribute)} property.");

            return _typeToPrimaryKeyGetterMap[itemType] = new Func<object, object>((obj) => pkProperty.GetValue(obj));
        }

        public object GetPrimaryKey(object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var primaryKeyGetter = GetPrimaryKeyGetter(item.GetType());
            return primaryKeyGetter(item);
        }
    }
}
