using Models.HttpRequest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services;
using Services.BaseServices;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HttpRequestLayer.HttpRequest
{
    public class HttpProvider : IHttpProvider
    {
        #region Properties
        string Platform = (Device.RuntimePlatform == Device.iOS) ? "ios" : "android";
        string currentversion = VersionTracking.CurrentVersion;
        string lang = Thread.CurrentThread.CurrentCulture.Name.ToLower().Contains("ar") ? "ar" : "en";
        #endregion

        IToasterServices ToasterServices;
        public HttpProvider()
        {
            ToasterServices = ServicesGenerator.GetIToasterServices();
        }
        public static string CheckNullJsonObject(object obj)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                return obj == null ? string.Empty : JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<HttpBaseResponse<T>> GetReslt<T>(string root, object obj,string jWTToken,string baseUrl)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    httpClientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls;
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        string itm = JsonSerializationService.SerializeFunc(obj);
                          
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                         client.DefaultRequestHeaders.Add("Platform", Platform);
                        client.DefaultRequestHeaders.Add("Version", currentversion);
                        client.DefaultRequestHeaders.Add("Language", lang);
                        if (!root.ToLower().Contains("login"))
                        {
                            // client.DefaultRequestHeaders.Add("Bearer", Settings.JWTToken);
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jWTToken);
                        }

                        StringContent content = new StringContent(itm, Encoding.UTF8, "application/json");
                        var res = await client.PostAsync($"{baseUrl}{root}", content);
                        var tst_Content = await res?.Content?.ReadAsStringAsync();
                        var responseItm = JsonSerializationService.DeserializeObject<HttpBaseResponse<T>>(tst_Content);

                        if (res.StatusCode == HttpStatusCode.OK)//&& responseItm?.StatusCode == HttpStatusCode.OK)
                        {
                            return responseItm;
                        }

                        else if (res.StatusCode == HttpStatusCode.BadRequest && responseItm?.Errors != null && responseItm?.Errors.Count != 0)
                        {
                            StringBuilder str = new StringBuilder();
                            for (int i = 0; i < responseItm?.Errors.Count; i++)
                            {
                                str.Append(responseItm?.Errors[0].Value);
                                str.Append("\n");
                            }
                            ToasterServices.ShowAlertmessage(str.ToString());

                        }

                        else
                        {
                            //Settings.JWTToken = string.Empty;
                            ToasterServices.ShowAlertmessage("un Authorized");
                            //  Application.Current.MainPage = new NavigationPage(new LoginPage());
                        }

                    }

                }
            }
            catch (Exception Excep)
            {
                var Res = Excep.Message;
            }
            return default;
        }

        public async Task<HttpBaseResponse<T>> Post_Upload<T>(FileResult ImgFile = null,string jWTToken="",string url= "")
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                httpClientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls;

                using (var client = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        var cul = Thread.CurrentThread.CurrentCulture.Name;
                         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Platform", Platform);
                        client.DefaultRequestHeaders.Add("Version", currentversion);
                        client.DefaultRequestHeaders.Add("Language", lang);

                        //client.DefaultRequestHeaders.Add("Bearer", Settings.JWTToken);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jWTToken);

                        var strm = await ImgFile.OpenReadAsync();
                        var requestContent = new MultipartFormDataContent();

                        var fileContent = new ByteArrayContent(GetByteFromStream.ReadFully(strm));
                        var MultiCont = new MultipartFormDataContent();

                        //  fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                        var fileType = ImgFile.ContentType;
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(fileType);
                        requestContent.Add(fileContent, "uploads", ImgFile.FileName);
                        requestContent.Add(new StringContent("normal"), "type");

                        var res = await client.PostAsync($"{url}uploads/upload_files", requestContent);
                        string str = await res.Content.ReadAsStringAsync();
                        var responseItm = JsonSerializationService.DeserializeObject<HttpBaseResponse<T>>(str);

                        return responseItm;

                    }
                    catch (Exception Excep)
                    {


                    }
                }

            }

            return default;
        }

    }

}
