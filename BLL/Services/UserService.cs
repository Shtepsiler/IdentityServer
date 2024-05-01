using AutoMapper;
using BLL.Configurations;
using BLL.DTO.Requests;
using BLL.DTO.Responses;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Entities;
using DAL.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly IConfiguration configuration;
        private readonly ClientAppConfiguration client;
        private readonly AppDBContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly EmailSender emailSender;
        private readonly RoleManager<Role> roleManager;

        public UserService(IMapper mapper, ITokenService tokenService, AppDBContext dbContext, UserManager<User> userManager, EmailSender emailSender, IConfiguration configuration, ClientAppConfiguration clientAppConfiguration, RoleManager<Role> roleManager)
        {
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.configuration = configuration;
            this.client = clientAppConfiguration;
            this.roleManager = roleManager;
        }

        public async Task ResetPassword(ResetPasswordRequest request)
        {
   
            var user = await userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                // Don't reveal that the user does not exist
                throw new EntityNotFoundException("User not found");
            }
            var result = await userManager.ResetPasswordAsync(user, request.Code, request.NewPasword);
            
            if (!result.Succeeded)
            {
                throw new ArgumentException(string.Join("\n",
                    result.Errors.Select(error => error.Description)));
            }

        }

        public async Task ForgotPassword(ForgotPasswordRequest  request)
        {
            var user = await userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
            if (user == null || !(await userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false)))
                throw new Exception("Please verify your email address.");

            var code = await userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
         
            var callbackUrl = $"{client.Url}{client.ResetPasswordPath}?Id={user.Id}&Code={code}";
            await emailSender.SendEmailAsync(request.Email, "Reset password", callbackUrl);

        }
        public async Task AsignRole(Guid id, string roleName)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    // Якщо роль не існує, створити її
                    role = new Role(roleName);
                    await roleManager.CreateAsync(role);
                }

                // Присвоїти роль користувачеві
                await userManager.AddToRoleAsync(user, roleName);
            }
            catch (ObjectDisposedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<UserResponse> GetClientById(Guid Id)
        {
            string id = Id.ToString();
            var user = await userManager.FindByIdAsync(id);

            if (user == null) throw new Exception();

            return mapper.Map<UserResponse>(user);
        }

        public async Task UpdateAsync(Guid Id, UserRequest client)
        {
            var user = await userManager.FindByIdAsync(Id.ToString());

            if (user == null) throw new Exception();

            user.UserName = client.UserName;
            user.PhoneNumber = client.PhoneNumber;
            user.Email = client.Email;



            await userManager.UpdateAsync(user);

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var user = await userManager.FindByIdAsync(Id.ToString());
            if (user == null) throw new Exception();

            await userManager.DeleteAsync(user);
            await dbContext.SaveChangesAsync();
        }
    }

}
