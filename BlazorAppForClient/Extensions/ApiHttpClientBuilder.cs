using BlazorAppForClient.Authentication;
using System.Net.Http;

namespace BlazorAppForClient.Extensions
{
    public class ApiHttpClientBuilder
    {
        private readonly HttpClient httpClient;

        private ApiAuthenticationStateProvider stateProvider;

        public ApiHttpClientBuilder AddAuthorization(ApiAuthenticationStateProvider stateProvider)
        {
            this.stateProvider = stateProvider;
            return this;
        }

        public ApiHttpClient Build() => new(httpClient, stateProvider);

        public ApiHttpClientBuilder(HttpClient httpClient) => this.httpClient = httpClient;
    }
}
