
using BLL.DTO.Requests;
using BLL.DTO.Responses;
using BLL.Services.Interfaces;
using DAL.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
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
        public IdentityController(IValidator<UserSignInRequest> singinvalidator, IValidator<UserSignUpRequest> singupvalidator, IIdentityService identityService)
        {
            _SingInValidator = singinvalidator;
            _SingUpValidator = singupvalidator;
            _IdentityService = identityService;
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
                var valid = _SingInValidator.Validate(request);

                if (request == null) { throw new ArgumentNullException(nameof(request)); }
                if (!valid.IsValid) { throw new ValidationException(valid.Errors); }

                var response = await _IdentityService.SignInAsync(request);
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
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim =>
            new {
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
            });

            return Ok(claims);

                 
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
                if (!_SingUpValidator.Validate(request).IsValid) { throw new Exception(nameof(request)); }

                var response = await _IdentityService.SignUpAsync(request);

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
