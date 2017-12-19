using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model.Data
{
    public interface IDataRelation
    {
        object GetPrimaryKey(object item);
    }
}
