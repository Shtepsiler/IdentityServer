using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAppForClient.Authentication
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;

        private static AuthenticationState AnonymousState =>
            new(new ClaimsPrincipal(new ClaimsIdentity()));

        public async Task<string> GetJwtTokenAsync() =>
            await localStorage.GetItemAsync<string>("securityToken");
        public async Task<int> GetClientIdAsync() =>
            await localStorage.GetItemAsync<int>("clientId");
      /*  public async Task<string> GetRefreshTokenAsync() =>
            await localStorage.GetItemAsync<string>("refreshToken");*/
        public async Task<string> GetClientNameAsync() =>
    await localStorage.GetItemAsync<string>("clientName");
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var encryptedToken = await localStorage.GetItemAsync<string>("securityToken");
            if (encryptedToken is null)
            {
                return AnonymousState;
            }

            var token = new JwtSecurityTokenHandler().ReadJwtToken(encryptedToken);
            return GenerateStateFromToken(token);
        }

        public async Task RenewAccessToken(string encryptedToken)
        {
            try
            {
                var Token = await localStorage.GetItemAsync<string>("securityToken");
                if (Token is not null)
                {
                    await localStorage.RemoveItemAsync("securityToken");

                }

                await localStorage.SetItemAsync("securityToken", encryptedToken);

            }
            catch (Exception e){ throw e; }


            var token = new JwtSecurityTokenHandler().ReadJwtToken(encryptedToken);
            var state = GenerateStateFromToken(token);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }
        public async Task MarkUserAsAuthenticatedAsync(string encryptedToken,int userId,string clientName)
        {
            await localStorage.SetItemAsync("securityToken", encryptedToken);
            await localStorage.SetItemAsync("clientId", userId);
           // await localStorage.SetItemAsync("refreshToken", refreshToken);
            await localStorage.SetItemAsync("clientName", clientName);



            var token = new JwtSecurityTokenHandler().ReadJwtToken(encryptedToken);
            var state = GenerateStateFromToken(token);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await localStorage.RemoveItemAsync("securityToken");
            await localStorage.RemoveItemAsync("clientId");
            await localStorage.RemoveItemAsync("clientName");

            NotifyAuthenticationStateChanged(Task.FromResult(AnonymousState));
        }
        
        public static async Task<string> GetUserIdFromStateAsync(Task<AuthenticationState> state) =>
            (await state).User.Claims.First(claim => claim.Type == ClaimTypes.Authentication).Value;

        private static AuthenticationState GenerateStateFromToken(JwtSecurityToken token)
        {
            var identity = new ClaimsIdentity(token.Claims, "apiauth_type");
            var principal = new ClaimsPrincipal(identity);
            return new(principal);
        }

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage) =>
            this.localStorage = localStorage;
    }
}
