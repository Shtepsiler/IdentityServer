
using BLL.Configurations;
using BLL.DTO.Requests;
using BLL.DTO.Responses;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Exceptions;
using FluentValidation;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IValidator<UserSignUpRequest> _SingUpValidator;
        private IValidator<UserSignInRequest> _SingInValidator;
        private readonly IIdentityService _IdentityService;
        private readonly GoogleClientConfiguration googleClientConfiguration;
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;

        public IdentityController(IValidator<UserSignInRequest> singinvalidator, IValidator<UserSignUpRequest> singupvalidator, IIdentityService identityService, GoogleClientConfiguration googleClientConfiguration, UserManager<User> userService, ITokenService tokenService)
        {
            _SingInValidator = singinvalidator;
            _SingUpValidator = singupvalidator;
            _IdentityService = identityService;
            this.googleClientConfiguration = googleClientConfiguration;
            this.userManager = userService;
            this.tokenService = tokenService;
        }

        [HttpPost("signIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JwtResponse>> SignInAsync(
            [FromBody] UserSignInRequest request)
        {
            try
            {               
                if (request == null) { throw new ArgumentNullException(nameof(request)); }

                var valid = _SingInValidator.Validate(request);

                if (!valid.IsValid) { throw new ValidationException(valid.Errors); }

                var response = await _IdentityService.SignInAsync(request);

                HttpContext.Response.Cookies.Append("Bearer", response.Token, new() {
                Expires = DateTime.Now.AddDays(2),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
                });


                return Ok(response);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        [HttpPost("LoginWithGoogle")]

        public async Task<ActionResult<JwtResponse>> LoginWithGoogle([FromBody] string credentials)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { googleClientConfiguration.GoogleClientID }
                

            };


            var peyload = await GoogleJsonWebSignature.ValidateAsync(credentials, settings);

            
            var user = userManager.FindByEmailAsync(email: peyload.Email).Result;

           if(user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "user not found");
            }
            var jwtToken = tokenService.BuildToken(user);
            HttpContext.Response.Cookies.Append("Bearer", tokenService.SerializeToken(jwtToken), new()
            {
                Expires = DateTime.Now.AddDays(2),
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });
            return  Ok(new JwtResponse() { Id = user.Id, Token = tokenService.SerializeToken(jwtToken),  ClientName= user.UserName });

        }

        [HttpPost("SignInWitGoogleAsync")]
        public async Task<IActionResult> SignInWitGoogleAsync()
        {
            try
            {
                await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                    new AuthenticationProperties { RedirectUri = Url.Action("GoogleResopnse") });
                return NoContent();
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }


        [HttpPost("GoogleResopnse")]
        public async Task<IActionResult> GoogleResopnse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (result != null)
                {
                    var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim =>
                    new
                    {
                        claim.Issuer,
                        claim.OriginalIssuer,
                        claim.Type,
                        claim.Value
                    });

                    return Ok(claims);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost("signUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JwtResponse>> SignUpAsync(
         [FromBody] UserSignUpRequest request)
        {
            try
            {
                if (request == null) { throw new ArgumentNullException(nameof(request)); }

                var valid = _SingUpValidator.Validate(request);
                if (!valid.IsValid) { throw new ValidationException(valid.Errors); }

                var response = await _IdentityService.SignUpAsync(request);

                HttpContext.Response.Cookies.Append("Bearer", response.Token, new()
                {
                    Expires = DateTime.Now.AddDays(2),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }






        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            try
            {
                if (request == null) { throw new ArgumentNullException(nameof(request)); }


                await _IdentityService.ConfirmEmail(request);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (EmailNotConfirmedException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }





    }
}
