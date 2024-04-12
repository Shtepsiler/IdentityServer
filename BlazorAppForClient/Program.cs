using BlazorAppForClient.Authentication;
using BlazorAppForClient.Extensions;
using BlazorAppForClient.Interfaces;
using BlazorAppForClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage(); 
builder.Services.AddMudServices();
builder.Services.AddAuthenticationCore();
/*builder.Services.AddAutoMapper(typeof(MapperProfile));
*/
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});



string APIBaseString = builder.Configuration["APIBaseString"];
/*builder.Services.AddMvc()
                    .AddFluentValidation(configuration =>
                    {
                        configuration.RegisterValidatorsFromAssemblies(
                            AppDomain.CurrentDomain.GetAssemblies());
                    });*/

builder.Services.AddTransient<RequestErrorsHandler>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
                provider => provider.GetRequiredService<ApiAuthenticationStateProvider>());



/*builder.Services.AddScoped<ApiHttpClient>();*/

builder.Services.AddHttpClient<IJobService, JobService>(httpClient =>
{
    httpClient.BaseAddress = new($"{APIBaseString}Job/");
});
builder.Services.AddHttpClient<ITeamService, TeamService>(httpClient =>
{
    httpClient.BaseAddress = new($"{APIBaseString}Mechanic/");
});
builder.Services.AddHttpClient<IIdentityService, IdentityService>(httpClient =>
{
    httpClient.BaseAddress = new($"{APIBaseString}Identity/");
});
builder.Services.AddHttpClient<IUsersService, UsersService>(httpClient =>
{
    httpClient.BaseAddress = new($"{APIBaseString}Client/");
});

builder.Services.AddMvc();

builder.Services.AddScoped<IAsyncAuthorizationFilter,JwtAuthorizationFilter>();
/*
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/manage");
    options.Conventions.AuthorizePage("/checkmyorders");
    options.Conventions.AuthorizePage("/makeanappointment");







    // Додайте інші сторінки, які потребують авторизації
});
*/


var app = builder.Build();
/*
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 401)
    {

        context.HttpContext.Response.Redirect("/expiredTokenRedirect");
    }
});*/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
