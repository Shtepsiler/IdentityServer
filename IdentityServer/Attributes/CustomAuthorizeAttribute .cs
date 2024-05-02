using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace IdentityServer.Attributes
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

                if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Отримати ID користувача з токена
            var userIdFromToken = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Отримати ролі користувача
            var userRoles = context.HttpContext.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            var roles = Roles.Split(" ").ToList();
            // Отримати запитуваний ID з запиту
            var requestedId = context.HttpContext.Request.RouteValues["Id"]?.ToString();

            // Якщо користувач є членом зазнаенних ролей або запитує дані про себе, то дозволити доступ
            if (userRoles.Any(r => roles.Contains(r)) || requestedId == userIdFromToken)
            {
                return;
            }
            else
            {
                // Якщо користувач не адміністратор і не запитує дані про себе, повернути заборону
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
