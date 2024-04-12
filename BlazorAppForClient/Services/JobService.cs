using BlazorAppForClient.Authentication;
using BlazorAppForClient.Extensions;
using BlazorAppForClient.Interfaces;
using BlazorAppForClient.ViewModels;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlazorAppForClient.Services
{
    public class JobService : IJobService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiAuthenticationStateProvider _stateProvider;

        public JobService( HttpClient httpClient, ApiAuthenticationStateProvider stateProvider) {
            _httpClient = httpClient;
            _stateProvider = stateProvider;
        }

        public async Task<IEnumerable<CompleteJobViewModel>> GetAsync()
        {
            return await GetAsync<IEnumerable<CompleteJobViewModel>>();
        }
        private async Task<T> GetAsync<T>()
        {
            _httpClient.DefaultRequestHeaders.Authorization = await GenerateAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync($"user/{ await _stateProvider.GetClientIdAsync()}");
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
            return JsonSerializer.Deserialize<T>(responseBody);
        }

        /*        private async Task<JobViewModel> ExecuteRequestAsync<T>(string requestUri, T model)
                {
                    var jwtModel = await _httpClient.PostWithoutAuthorizationAsync<T, JobViewModel>(
                        requestUri,
                        model);

                    return jwtModel;
                }*/
        public async Task PostAsync(string requestUri, NewJoobViewModel viewModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = await GenerateAuthorizationHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync(requestUri, viewModel);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
   /*         if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized )
            {
*//*                _IdentityService.TryRefreshTokenAsync();
*//*                _httpClient.DefaultRequestHeaders.Authorization = await GenerateAuthorizationHeaderAsync();
                var newresponse = await _httpClient.PostAsJsonAsync(requestUri, viewModel);
                var newresponseBody = await newresponse.Content.ReadAsStringAsync();
                StatusCodeHandler.TryHandleStatusCode(newresponse.StatusCode, newresponseBody);



            }*/




        }
        private async Task<AuthenticationHeaderValue> GenerateAuthorizationHeaderAsync() =>
    new("Bearer", await _stateProvider.GetJwtTokenAsync());



        public async Task SubmitAJob(NewJoobViewModel newJoobViewModel)
        {
            newJoobViewModel.ClientId = await _stateProvider.GetClientIdAsync();
            await PostAsync("", newJoobViewModel);
        }
    }
}
