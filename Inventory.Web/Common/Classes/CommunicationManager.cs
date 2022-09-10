using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Inventory.Web.Common.Classes
{
    public class CommunicationManager
    {
        /// <summary>
        /// Http client to communicate with rest service
        /// </summary>
        private readonly HttpClient _httpClient;

        public CommunicationManager()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// This Method is used to send web api GET request
        /// </summary>
        /// <typeparam name="T1">This type is used to send generic parameter.</typeparam>
        /// <typeparam name="T2">This type is used as the generic return type</typeparam>
        /// <param name="apiUri">This is used to send webapi url</param>
        /// <param name="input">This is used to send parameters</param>
        ///// <returns>T2</returns>
        //public T2 Get<T1, T2>(string apiUri, T1 input)
        //{
        //    var urlEncodedParams = GetUrlEncodedHttpContent(input);
        //    Uri uri = new Uri(
        //       String.Format("{0}{1}", apiUri, urlEncodedParams)
        //       );
        //    var response = _httpClient.GetAsync(uri).Result;
        //    response.EnsureSuccessStatusCode();
        //    //return JsonConvert.DeserializeObject<T2>(response.Content.ReadAsStringAsync().Result);         
        //    return response.Content.ReadAsAsync<T2>().Result;

        //}
        /// <summary>
        /// This Method is used to send web api GET request
        /// </summary>
        /// <typeparam name="T1">This type is used to send generic parameter.</typeparam>
        /// <typeparam name="T2">This type is used as the generic return type</typeparam>
        /// <param name="apiUri">This is used to send webapi url</param>
        /// <param name="input">This is used to send parameters</param>
        /// /// <param name="Token">This is used to send Token</param>
        /// <returns>T2</returns>
        //public T2 Get<T1, T2>(string apiUri, T1 input, string Token)
        //{
        //    var urlEncodedParams = GetUrlEncodedHttpContent(input);
        //    Uri uri = new Uri(
        //       String.Format("{0}{1}", apiUri, urlEncodedParams)
        //       );
        //    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
        //    var response = _httpClient.GetAsync(uri).Result;
        //    response.EnsureSuccessStatusCode();
        //    //return JsonConvert.DeserializeObject<T2>(response.Content.ReadAsStringAsync().Result);         
        //    return response.Content.ReadAsAsync<T2>().Result;

        //}

        /// <summary>
        ///  This Method is used to send web api GET request
        /// </summary>
        /// <typeparam name="T1">This type is used as the generic return type</typeparam>
        /// <param name="apiUri">This is used to send webapi url</param>
        /// <returns></returns>
        public T1 Get<T1>(string apiUri)
        {
            Uri uri = new Uri( String.Format("{0}", apiUri));

            var response = _httpClient.GetAsync(uri).Result;
            return response.Content.ReadAsAsync<T1>().Result;
        }

        /// <summary>
        /// This Method is used to send web api GET request
        /// </summary>
        /// <typeparam name="T1">This type is used as the generic return type</typeparam>
        /// <param name="apiUri">This is used to send webapi url</param>
        /// <param name="Token">This is used to send Token</param>
        /// <returns>T1</returns>
        public T1 Get<T1>(string apiUri, string Token)
        {
            Uri uri = new Uri(
                                   String.Format("{0}", apiUri)
                                   );
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);

            var response = _httpClient.GetAsync(uri).Result;
            return response.Content.ReadAsAsync<T1>().Result;
        }

        /// <summary>
        /// This Method is used to send web api POST request
        /// </summary>
        /// <typeparam name="T1">This type is used to send generic parameter.</typeparam>
        /// <typeparam name="T2">This type is used as the generic return type</typeparam>
        /// <param name="apiUri">This is used to send webapi url</param>
        /// <param name="input">This is used to send parameters</param>
        /// <returns>T2</returns>
        public T2 Post<T1, T2>(string apiUri, T1 input)
        {

            try
            {
                var response = _httpClient.PostAsJsonAsync(apiUri, input).Result;
                response.EnsureSuccessStatusCode();
                //return JsonConvert.DeserializeObject<T2>(response.Content.ReadAsStringAsync().Result);         
                return response.Content.ReadAsAsync<T2>().Result;
            }
            catch (HttpRequestException ex)
            {

                throw;
            }
            catch (Exception ex1)
            {

                throw;
            }
        }


        /// <summary>
        /// This Method is used to send web api POST request
        /// </summary>
        /// <typeparam name="T1">This type is used to send generic parameter.</typeparam>
        /// <typeparam name="T2">This type is used as the generic return type</typeparam>
        /// <param name="apiUri">This is used to send webapi url</param>
        /// <param name="input">This is used to send parameters</param>
        /// /// <param name="Token">This is used to send Token</param>
        /// <returns>T2</returns>
        public T2 Post<T1, T2>(string apiUri, T1 input, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var response = _httpClient.PostAsJsonAsync(apiUri, input).Result;
            //response.EnsureSuccessStatusCode();
            //return JsonConvert.DeserializeObject<T2>(response.Content.ReadAsStringAsync().Result);         
            return response.Content.ReadAsAsync<T2>().Result;
        }



        //public T2 Put<T1, T2>(string apiUri, T1 input)
        //{
        //    var response = _httpClient.PutAsync(apiUri, GetFormUrlEncodedHttpContent<T1>(input)).Result;
        //    response.EnsureSuccessStatusCode();
        //    return response.Content.ReadAsAsync<T2>().Result;
        //}
        public T1 Delete<T1>(string apiUri)
        {

            var response = _httpClient.DeleteAsync(apiUri).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<T1>().Result;
        }


        /// <summary>
        /// This Method is used to get Url encoded HttpContent
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        //private string GetUrlEncodedHttpContent<T1>(T1 input)
        //{
        //    var objType = input.GetType();
        //    var paramBuilder = new StringBuilder();
        //    const string paramFormat = "{0}={1}&";
        //    foreach (var property in objType.GetProperties().Where(property => !property.PropertyType.IsArray))
        //    {
        //        var value = property.GetValue(input);
        //        if (null != value)
        //            paramBuilder.Append(string.Format(paramFormat, property.Name, value));
        //    }
        //    return paramBuilder.ToString().TrimEnd('&').Insert(0, "?");
        //}

        //private HttpContent GetFormUrlEncodedHttpContent<T1>(T1 input)
        //{
        //    HttpContent content1;
        //    var postData = new List<KeyValuePair<string, string>>();
        //    var objType = input.GetType();
        //    foreach (var property in objType.GetProperties().Where(property => !property.PropertyType.IsArray))
        //    {
        //        var value = property.GetValue(input);
        //        if (null != value)
        //            postData.Add(new KeyValuePair<string, string>(property.Name, Convert.ToString(value)));
        //    }
        //    content1 = new FormUrlEncodedContent(postData);
        //    return content1;
        //}

        //public AccessToken GetToken(string tokenUrl, string requestDetails)
        //{
        //    WebRequest webRequest = WebRequest.Create(tokenUrl);
        //    webRequest.ContentType = "application/x-www-form-urlencoded";
        //    webRequest.Method = "POST";
        //    byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
        //    webRequest.ContentLength = bytes.Length;
        //    using (Stream outputStream = webRequest.GetRequestStream())
        //    {
        //        outputStream.Write(bytes, 0, bytes.Length);
        //    }
        //    try
        //    {
        //        using (WebResponse webResponse = webRequest.GetResponse())
        //        {
        //            StreamReader newstreamreader = new StreamReader(webResponse.GetResponseStream());
        //            string newresponsefromserver = newstreamreader.ReadToEnd();
        //            var token = new JavaScriptSerializer().Deserialize<AccessToken>(newresponsefromserver);
        //            return token;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.Message;
        //        AccessToken at = new AccessToken();
        //        at.message = message;
        //        return at;
        //    }
        //}
    }
}
