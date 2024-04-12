using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
namespace BlazorAppForClient.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {


        [Route("SignInWitGoogleAsync")]

        [HttpPost("SignInWitGoogleAsync")]
        public async Task SignInWitGoogleAsync()
        {
            try
            {
                await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                    new AuthenticationProperties { RedirectUri = "https://localhost:7072/api/Identity/GoogleResopnse" });
            }

            catch (Exception e)
            {
                throw e;
            }
        }
        [Route("GoogleResopnse")]

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








    }
}
