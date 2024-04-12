using BlazorAppForClient.Authentication;
using BlazorAppForClient.Extensions;
using BlazorAppForClient.Interfaces;
using BlazorAppForClient.ViewModels;

using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorAppForClient.Controllers;


namespace BlazorAppForClient.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApiHttpClient httpClient;

        private readonly ApiAuthenticationStateProvider stateProvider;
        private readonly HttpContext HttpContext;
        private readonly IUrlHelper Url;
        private readonly IdentityController identityController;
        public async Task<JwtViewModel> SignInAsync(UserSignInViewModel viewModel) =>
            await ExecuteRequestAsync("signIn", viewModel);

        public async Task<JwtViewModel> SignUpAsync(UserSignUpViewModel viewModel) =>
            await ExecuteRequestAsync("signUp", viewModel);

        public async Task SignInWithGoogleAsync()
        {
            identityController.SignInWitGoogleAsync();
        }/*=>

       await ExecuteAsync("SignInWitGoogleAsync");*/


        private async Task<JwtViewModel> ExecuteRequestAsync<T>(string requestUri, T? model)
        {
            var jwtModel = await httpClient.PostWithoutAuthorizationAsync<T, JwtViewModel>(
                requestUri,
                model);

            await stateProvider.MarkUserAsAuthenticatedAsync(jwtModel.token,jwtModel.id,jwtModel.clientName);
            return jwtModel;
        }
        private async Task ExecuteAsync(string requestUri)
        {
           await httpClient.PostAsync(requestUri);

        }
        public async Task SingOutAsync()
        {
            await stateProvider.MarkUserAsLoggedOutAsync();
        }
      /*  public async Task TryRefreshTokenAsync()
        {
            var jwt = await ExecuteRequestRefreshTokenAsync("refreshToken");

            stateProvider.MarkUserAsAuthenticatedAsync(jwt.token, jwt.id, jwt.refreshToken);




        }*/



        public IdentityService(
            HttpClient httpClient,
            ApiAuthenticationStateProvider stateProvider,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper url,
            IdentityController identityController)
        {
            this.httpClient = new ApiHttpClientBuilder(httpClient).AddAuthorization(stateProvider)
                                                                  .Build();

            this.stateProvider = stateProvider;
            HttpContext = httpContextAccessor.HttpContext;
            Url = url;
            this.identityController = identityController;
        }
    }
}
