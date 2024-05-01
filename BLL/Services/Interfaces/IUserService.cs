using BLL.DTO.Requests;
using BLL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        // Task AddPhoneNumber();


        Task<UserResponse> GetClientById(Guid Id);
       // Task RewokeRefreshToken(string clientname, string token);

       // Task<JwtResponse> RenewAccesToken(string refreshtoken);

        Task UpdateAsync(Guid Id, UserRequest client);
        Task DeleteAsync(Guid Id);
        Task ResetPassword(ResetPasswordRequest request);
        Task ForgotPassword(ForgotPasswordRequest request);
       
       // Task AsignRole(Guid Id, string role);
    }
}
