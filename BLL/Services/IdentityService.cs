using AutoMapper;
using BLL.Configurations;
using BLL.DTO.Requests;
using BLL.DTO.Responses;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Entities;
using DAL.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;


namespace BLL.Services
{
    public class IdentityService : IIdentityService
    {

        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly DBContext dbContext;
        private readonly ITokenService tokenService;
        private readonly EmailSender emailSender;
        private readonly ClientAppConfiguration client;
        private readonly ILogger<IdentityService> logger;

        public IdentityService(
        IMapper mapper,
        ITokenService tokenService,
        UserManager<User> userManager,
        DBContext dbContext
,
        EmailSender emailSender,
        ClientAppConfiguration client,
        ILogger<IdentityService> logger)
        {
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.emailSender = emailSender;
            this.client = client;
            this.logger = logger;
        }
        public async Task ConfirmEmail(ConfirmEmailRequest request)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString())
                  ?? throw new EntityNotFoundException(
                      $"{nameof(User)} with user ID {request.Id} not found.");

            var rez = await userManager.ConfirmEmailAsync(user,request.Code);
            
            if (rez.Succeeded)
            {
                user.EmailConfirmed = true;
            await dbContext.SaveChangesAsync();

            }
            else
            {
                throw new EmailNotConfirmedException("Email is not confirmed");
            }  

        }

        public async Task<JwtResponse> SignInAsync(UserSignInRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName)
                ?? throw new EntityNotFoundException(
                    $"{nameof(User)} with user name {request.UserName} not found.");

            if (!await userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new EntityNotFoundException("Incorrect username or password.");
            }
            if (!user.EmailConfirmed)
            {
                var userId = await userManager.GetUserIdAsync(user);
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
               // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = $"{client.Url}{client.EmailConfirmationPath}?Id={userId}&Code={System.Net.WebUtility.UrlEncode(code)}";

                await emailSender.SendEmailAsync(user.Email, "Confirm your email",
   $"{client.ResetPasswordMessage}{callbackUrl}");
            }
            logger.Log(LogLevel.Information,$"User {request.UserName} is Sign in");

            var jwtToken = tokenService.BuildToken(user);
            return new() { Id = user.Id, Token = tokenService.SerializeToken(jwtToken), ClientName = user.UserName};
        }

        public Task SignOutAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<JwtResponse> SignUpAsync(UserSignUpRequest request)
        {
            var user = mapper.Map<UserSignUpRequest, User>(request);
            var signUpResult = await userManager.CreateAsync(user, request.Password);

            if (!signUpResult.Succeeded)
            {
                string errors = string.Join("\n",
                    signUpResult.Errors.Select(error => error.Description));

                throw new ArgumentException(errors);
            }

            await dbContext.SaveChangesAsync();
            var newClient = await userManager.FindByNameAsync(request.UserName);


            if (!user.EmailConfirmed)
            {
                var userId = await userManager.GetUserIdAsync(user);
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
              //  code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = $"{client.Url}{client.EmailConfirmationPath}?Id={userId}&Code={code}";


                await emailSender.SendEmailAsync(user.Email, "Confirm your email",
   $"{client.ResetPasswordMessage}{callbackUrl}");
            }
            logger.Log(LogLevel.Information, $"User {request.UserName} is Sign Up");


            try
            {
                //  var newClient = await userManager.FindByNameAsync(request.UserName);
                var jwtToken = tokenService.BuildToken(user);
                return new() { Id = newClient.Id, ClientName = newClient.UserName, Token = tokenService.SerializeToken(jwtToken)};
            }
            catch (Exception ex) { throw new Exception("database troble"); }
        }


    }
}
