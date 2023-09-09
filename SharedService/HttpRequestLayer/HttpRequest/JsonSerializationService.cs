using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace  HttpRequestLayer
{
    public class JsonSerializationService
    {
        public static string SerializeFunc(object itm)
        {
            try
            {
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(),

                };
                return JsonConvert.SerializeObject(itm, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });
            }
            catch (System.Exception Execp)
            {

            }
            return "";
        }
        public static T DeserializeObject<T>(string content)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                var itm = JsonConvert.DeserializeObject<object>(content, serializerSettings);

                var obj = JsonConvert.DeserializeObject<T>(content, serializerSettings);

                return JsonConvert.DeserializeObject<T>(content, serializerSettings);
            }
            catch (Exception Excep)
            {

            }
            return default;
        }
    }

}
