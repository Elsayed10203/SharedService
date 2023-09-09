using Newtonsoft.Json;

namespace Models.HttpRequest
{
    public class HttpBaseSuccess
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }
    }

      
}
