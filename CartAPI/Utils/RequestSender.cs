using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CartAPI.Utils
{
    public class RequestSender
    {
        private string BaseUrl { get; set; }
        public void SetBaseUrl(string baseUrl) => BaseUrl = baseUrl;
        public async Task<(string,int)> PostRequestAsync(string url, bool isAbsoluteUrl, dynamic body, Dictionary<string, object> headers = null)
        {
            var Requestbody = new StringContent(JsonSerializer.Serialize(body));
            var contentType = "application/json";
            var resp = await SendRequestAsync(url, isAbsoluteUrl, HttpMethod.Post, Requestbody, contentType, headers: headers);
            var responseBody = await resp.Content.ReadAsStringAsync();
            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(TResponse, int)> PostRequestAsync<TResponse>(string url, bool isAbsoluteUrl, dynamic body, Dictionary<string, object> headers = null)
        {
            var Requestbody = new StringContent(JsonSerializer.Serialize(body));
            var contentType = "application/json";
            var resp = await SendRequestAsync(url, isAbsoluteUrl, HttpMethod.Post, Requestbody, contentType, headers: headers);
            var responseBody = JsonSerializer.Deserialize<TResponse>(await resp.Content.ReadAsStringAsync());
            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(string, int)> PutRequestAsync(string url, bool isAbsoluteUrl, dynamic body, Dictionary<string, object> headers = null)
        {
            var requestbody = new StringContent(JsonSerializer.Serialize(body));
            var contentType = "application/json";
            var resp = await SendRequestAsync(url, isAbsoluteUrl, HttpMethod.Put, requestbody, contentType, headers: headers);
            var responseBody = await resp.Content.ReadAsStringAsync();
            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(TResponse, int)> PutRequestAsync<TResponse>(string url, bool isAbsoluteUrl, dynamic body, Dictionary<string, object> headers = null)
        {
            var requestbody = new StringContent(JsonSerializer.Serialize(body));
            var contentType = "application/json";
            var resp = await SendRequestAsync(url, isAbsoluteUrl, HttpMethod.Put, requestbody, contentType, headers: headers);
            var responseBody = JsonSerializer.Deserialize<TResponse>(await resp.Content.ReadAsStringAsync());
            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(string, int)> GetRequestAsync(string url, bool isAbsoluteUrl, Dictionary<string, string> parameters = null, Dictionary<string, object> headers = null)
        {
            var Requestbody = parameters != null ? new FormUrlEncodedContent(parameters) : null;
            var contentType = "application/x-www-form-urlencoded";
            var resp = await SendRequestAsync(url, isAbsoluteUrl, HttpMethod.Get, Requestbody, contentType, headers: headers);
            var responseBody = await resp.Content.ReadAsStringAsync();
            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(TResponse, int)> GetRequestAsync<TResponse>(string url, bool isAbsoluteUrl, Dictionary<string, string> parameters = null, Dictionary<string, object> headers = null)
        {
            var Requestbody = parameters != null ? new FormUrlEncodedContent(parameters) : null;
            var contentType = "application/x-www-form-urlencoded";
            var resp = await SendRequestAsync(url, isAbsoluteUrl, HttpMethod.Get, Requestbody, contentType, headers: headers);
            var responseBody = JsonSerializer.Deserialize<TResponse>(await resp.Content.ReadAsStringAsync());

            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(string, int)> GetRequestMutipartAsync(string url, bool isAbsoluteUrl, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null)
        {
            var resp = await SendMultiPartRequest(url, isAbsoluteUrl, HttpMethod.Get, parameters, headers);
            var responseBody = await resp.Content.ReadAsStringAsync();
            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(TResponse, int)> GetRequestMutipartAsync<TResponse>(string url, bool isAbsoluteUrl, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null)
        {
            var resp = await SendMultiPartRequest(url, isAbsoluteUrl, HttpMethod.Get, parameters, headers);
            var responseBody = JsonSerializer.Deserialize<TResponse>(await resp.Content.ReadAsStringAsync());
            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(string, int)> PostRequestMutipartAsync(string url, bool isAbsoluteUrl, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null)
        {
            var resp = await SendMultiPartRequest(url, isAbsoluteUrl, HttpMethod.Post, parameters, headers);
            var responseBody = await resp.Content.ReadAsStringAsync();
            return (responseBody, (int)resp.StatusCode);
        }
        public async Task<(TResponse, int)> PostRequestMutipartAsync<TResponse>(string url, bool isAbsoluteUrl, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null)
        {
            var resp = await SendMultiPartRequest(url, isAbsoluteUrl, HttpMethod.Post, parameters, headers);
            var responseBody = JsonSerializer.Deserialize<TResponse>(await resp.Content.ReadAsStringAsync());
            return (responseBody, (int)resp.StatusCode);
        }
        private static async Task<HttpResponseMessage> SendMultiPartRequest(string url, bool isAbsoluteUrl, HttpMethod httpMethod, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null)
        {
            var multiPartFormData = new MultipartFormDataContent();
            foreach (var item in parameters)
            {
                if (item.Value is string)
                {
                    var formdata = new StringContent(item.Value.ToString());
                    formdata.Headers.ContentType = MediaTypeHeaderValue.Parse("text/plain");
                    formdata.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    multiPartFormData.Add(formdata, item.Key);
                }
                else if (item.Value is byte[])
                {
                    var formdata = new ByteArrayContent(item.Value as byte[]);
                    formdata.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    formdata.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    multiPartFormData.Add(formdata, item.Key);
                }
            }
            var Requestbody = parameters != null ? multiPartFormData : null;
            var contentType = "multipart/form-data";
            var resp = await SendRequestAsync(url, isAbsoluteUrl, httpMethod, Requestbody, contentType, headers: headers);
            return resp;
        }
        private static async Task<HttpResponseMessage> SendRequestAsync(string url, bool isAbsoluteUrl, HttpMethod httpMethod, HttpContent body, string contentType, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null, bool validateSsl = false)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{url}"),
                Method = httpMethod,
                Content = body
            };
            if (headers != null)
                foreach (var param in headers)
                    request.Headers.Add(param.Key, param.Value.ToString());
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            var clientHandlerRemoveSslValidation = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            using (var client = !validateSsl? new HttpClient(clientHandlerRemoveSslValidation) : new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                var response = await client.SendAsync(request);
                return response;
            }
        }
    }
}
