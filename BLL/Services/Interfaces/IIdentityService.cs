using BLL.DTO.Requests;
using BLL.DTO.Responses;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<JwtResponse> SignInAsync(UserSignInRequest request);

        Task<JwtResponse> SignUpAsync(UserSignUpRequest request);
        Task SignOutAsync(Guid id);
        Task ConfirmEmail(ConfirmEmailRequest request);
    }
}
