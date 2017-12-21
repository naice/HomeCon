using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model.Data
{
    public interface IDataAdapter
    {
        IEnumerable<DataModel> GetData<DataModel>()
            where DataModel : class, new();

        void Insert(object item, IDataRelation relation);
        void Delete(object item, IDataRelation relation);
        void Update(object item, IDataRelation relation);
    }
}
