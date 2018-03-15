using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ttpClient.Api.RequestModels;

namespace ttpClient.Api
{
    public class ApiRequestHelper
    {
        private const int RetryCount = 3;

        public async Task<HttpResponseMessage> GetHttpResponseFromRequest(ApiRequestParameters parameters)
        {
            if (parameters == null) return null;

            return await SendRequest(parameters.Url, parameters.Content, parameters.Method, parameters.MediaType, parameters.Token);

        }

        private async Task<HttpResponseMessage> SendRequest(string url, string content, HttpMethod method, string mediaType, string token)
        {
            if (string.IsNullOrEmpty(url)) return null;

            var uri = new Uri(url);
            var client = new HttpClient();
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            for (var i = 0; i < RetryCount; i++)
            {
                try
                {
                    if (method == HttpMethod.Get)
                    {
                        response = await client.GetAsync(uri);
                    }
                    else if (method == HttpMethod.Post || method == HttpMethod.Put)
                    {
                        if (content == null) return null;

                        response = await client.PostAsync(uri, new StringContent(content, Encoding.UTF8, mediaType));
                    }
                    else if (method == HttpMethod.Delete)
                    {
                        response = await client.DeleteAsync(uri);
                    }

                    if (response == null) continue;

                    if (response.IsSuccessStatusCode) return response;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return response;
        }
    }
}
