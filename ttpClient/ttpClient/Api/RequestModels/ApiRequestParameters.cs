using System.Net.Http;

namespace ttpClient.Api.RequestModels
{
    public class ApiRequestParameters
    {
        public string Url { get; set; }

        public string Content { get; set; }

        public HttpMethod Method { get; set; }

        public string MediaType { get; set; } = "application/json";

        public string Token { get; set; }
    }
}
