using BlazorAppForClient.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorAppForClient.Extensions
{
    public class JwtAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly NavigationManager _navigationManager;
        private readonly ApiAuthenticationStateProvider _stateProvider;
        public JwtAuthorizationFilter(NavigationManager navigationManager, ApiAuthenticationStateProvider stateProvider)
        {
            _navigationManager = navigationManager;
            _stateProvider = stateProvider;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Перевірка дійсності JWT-токена

            var token = new JwtSecurityTokenHandler().ReadJwtToken(await _stateProvider.GetJwtTokenAsync());

            if (token.ValidTo < DateTime.Now)
            {
                // Перенаправлення на сторінку входу
                _stateProvider.MarkUserAsLoggedOutAsync();
                _navigationManager.NavigateTo("/login");
            }
        }
    }
}
