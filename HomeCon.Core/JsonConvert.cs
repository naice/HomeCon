using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCon.Core
{
    class JsonConvert : RestServer.IConverter
    {
        public string ContentType => "application/json";

        object RestServer.IConverter.DeserializeObject(string data, Type type)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(data, type);
            }
            catch (Exception ex)
            {

                throw new RestServer.ConverterException(ex);
            }
        }
        string RestServer.IConverter.SerializeObject(object obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {

                throw new RestServer.ConverterException(ex);
            }
        }

        public static T DeserializeObject<T>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }
        public static string SerializeObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
