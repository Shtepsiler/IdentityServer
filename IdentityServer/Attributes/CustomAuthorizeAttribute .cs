using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
namespace IdentityServer.Attributes
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute, IAsyncAuthorizationFilter
    {

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerName = string.Empty;
            var methodName = string.Empty;   
            if (actionDescriptor != null)
            {
                // Отримати ім'я класу контролера
                 controllerName = actionDescriptor.ControllerName;

                // Отримати ім'я методу контролера
                 methodName = actionDescriptor.ActionName;

            }
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Отримати ID користувача з токена
            var userIdFromToken = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Отримати ролі користувача
            var userRoles = context.HttpContext.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

            // Отримати запитуваний ID з запиту
            var requestedId = context.HttpContext.Request.RouteValues["Id"]?.ToString();


            // Якщо користувач є адміном або запитує дані про себе, то дозволити доступ
            if (userRoles.Contains("admin") || requestedId == userIdFromToken)
            {

                Log.Logger.Information( $"user with id {userIdFromToken} try to {controllerName}.{methodName}({requestedId})  acces allowed");
                return;
            }
            else
            {
                Log.Logger.Information( $"user with id {userIdFromToken} try to {controllerName}.{methodName}({requestedId}) acces denited");
                // Якщо користувач не адміністратор і не запитує дані про себе, повернути заборону
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
