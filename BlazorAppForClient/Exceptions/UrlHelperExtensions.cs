using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace BlazorAppForClient.Exceptions
{
    public static class UrlHelperExtensions
    {
        public static IServiceCollection AddUrlHelper(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper>(x =>
            {
                var httpContextAccessor = x.GetRequiredService<IHttpContextAccessor>();
                var actionContext = new ActionContext(httpContextAccessor.HttpContext, httpContextAccessor.HttpContext.GetRouteData(), new ActionDescriptor());
                var urlHelperFactory = x.GetRequiredService<IUrlHelperFactory>();
                return urlHelperFactory.GetUrlHelper(actionContext);
            }); 
            services.AddHttpContextAccessor();
           /* services.AddScoped<IUrlHelper, UrlHelper>(serviceProvider =>
            {
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var actionContextAccessor = serviceProvider.GetRequiredService<IActionContextAccessor>();
                var urlHelperFactory = serviceProvider.GetRequiredService<IUrlHelperFactory>();
                return new UrlHelper(actionContextAccessor.ActionContext);
            });*/

            return services;
        }
    }
}
