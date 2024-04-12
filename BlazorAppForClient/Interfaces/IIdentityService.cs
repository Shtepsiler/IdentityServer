using BlazorAppForClient.ViewModels;
using System.Threading.Tasks;

namespace BlazorAppForClient.Interfaces
{
    public interface IIdentityService
    {
        Task<JwtViewModel> SignInAsync(UserSignInViewModel viewModel);
        Task SignInWithGoogleAsync();
        Task<JwtViewModel> SignUpAsync(UserSignUpViewModel viewModel);
/*        Task TryRefreshTokenAsync();
*/
        Task SingOutAsync();
    }
}
