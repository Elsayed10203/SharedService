using Models.HttpRequest;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HttpRequestLayer.HttpRequest
{
    public interface IHttpProvider
    {
        Task<HttpBaseResponse<T>> GetReslt<T>(string root, object obj, string jWTToken, string baseUrl);
        Task<HttpBaseResponse<T>> Post_Upload<T>(FileResult ImgFile = null, string jWTToken = "", string url = "");
    }

}

