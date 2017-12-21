using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model.Data
{
    public interface IDataRelation
    {
        object GetPrimaryKey(object item);
        Type GetPrimaryKeyType(object item);
        void SetPrimaryKey(object item, object newKey);
    }
}
