using BlazorAppForClient.Extensions;
using BlazorAppForClient.Interfaces;
using BlazorAppForClient.ViewModels;
using Blazored.LocalStorage;
using System.Text.Json;

namespace BlazorAppForClient.Services
{
    public class TeamService : ITeamService
    {
        private readonly HttpClient _httpClient;


        public TeamService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
/*        private async Task<T> GetAsync<T>(string requestUri)
        {
            //  httpClient.DefaultRequestHeaders.Authorization = await GenerateAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync(requestUri);
            var responseBody = await response.Content.ReadAsStringAsync();
            StatusCodeHandler.TryHandleStatusCode(response.StatusCode, responseBody);
            return JsonSerializer.Deserialize<T>(responseBody);
        }*/
        public async Task<IEnumerable<MechanicViewModel>> GetAllAsync()
        {
         
                var response = await _httpClient.GetAsync($"");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<MechanicViewModel>>(json);
            
        }


    }
}
