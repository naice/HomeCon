using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.RestServer
{
    public interface IConverter
    {
        string ContentType { get; }

        /// <summary>
        /// Serialize object. Should throw <see cref="ConverterException"/>
        /// </summary>
        string SerializeObject(object obj);

        /// <summary>
        /// Deserialize object from data. Should throw <see cref="ConverterException"/>
        /// </summary>
        object DeserializeObject(string data, Type type);
    }
}
