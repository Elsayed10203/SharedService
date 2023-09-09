using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Models.HttpRequest
{
    public class HttpBaseResponse<T>
    {
        [JsonProperty("error_code", NullValueHandling = NullValueHandling.Ignore)]
        public int ErrorCode { get; set; }

        [JsonProperty("status_code", NullValueHandling = NullValueHandling.Ignore)]
        public HttpStatusCode StatusCode { get; set; }
   
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<BaseError> Errors { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }
   
    public class HttpBasePagination
    {
        public bool has_pagination { get; set; }
        public int count_items { get; set; }
        public int count_items_in_current_page { get; set; }
        public int pages { get; set; }
    }

    public class BaseError
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Pagination<T>
    {
        public T List { get; set; }
        public int Pages { get; set; }
        public int Count { get; set; }
        public bool HasPagination { get; set; }
        public string Title { get; set; }

    }
}
