using BLL.Configurations;
using BLL.Factories.Interfaces;
using BLL.Factories;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BLL.Mapping;
using BLL.DTO.Requests;
using BLL.Services;
using FluentValidation;
using BLL.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using BLL.Services.Interfaces;
using BLL.MessageBroker;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSwaggerGen(o =>
    {
    
        o.SwaggerDoc("v1", new OpenApiInfo() { Title = "Identity Api" });
        o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme.",
        });
    
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //  o.IncludeXmlComments(xmlPath);
        // Security
        o.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                        });
    });

    builder.Services.AddDbContext<DBContext>(options =>
        {
        
            string connectionString;
        
            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true") {
        
                var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbname = Environment.GetEnvironmentVariable("DB_NAME");
                var dbpass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
        
        
                connectionString = $"Data Source={dbhost};User ID=sa;Password={dbpass};Initial Catalog={dbname};Encrypt=True;Trust Server Certificate=True;";
            }
            else
                connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
            options.UseSqlServer(connectionString);
            
        });
    
    builder.Services.AddAuthentication();
    builder.Services.AddIdentityCore<User>()
                       .AddRoles<Role>()
                       .AddUserManager<UserManager<User>>()
                       .AddSignInManager<SignInManager<User>>()
                       .AddRoleManager<RoleManager<Role>>()
                       .AddDefaultTokenProviders()
                       .AddEntityFrameworkStores<DBContext>();
    




    builder.Services.AddTransient<JwtTokenConfiguration>();
    builder.Services.AddTransient<IJwtSecurityTokenFactory, JwtSecurityTokenFactory>();
    
    builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

    builder.Services.AddTransient<ClientAppConfiguration>();

builder.Services.AddTransient<GoogleClientConfiguration>();

builder.Services.AddTransient<EmailSenderConfiguration>();
    builder.Services.AddTransient<EmailSender>();
        
    builder.Services.AddScoped<IValidator<UserSignInRequest>, UserSignInRequestValidator>();
    builder.Services.AddScoped<IValidator<UserSignUpRequest>, UserSingUpRequestValidator>();


    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IIdentityService, IdentityService>();
    builder.Services.AddScoped<IUserService, UserService>();





builder.Services.AddAuthentication(opt =>
{

    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme/*CookieAuthenticationDefaults.AuthenticationScheme*/;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme /*GoogleDefaults.AuthenticationScheme*/;

}).AddCookie(x =>
{
    x.Cookie.Name = "Bearer";
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"])),
        ClockSkew = TimeSpan.FromHours(1),
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["Bearer"];
            return Task.CompletedTask;
        }
    };



}).AddGoogle(GoogleDefaults.AuthenticationScheme, opt =>
{
    opt.ClientId = builder.Configuration["GoogleClientID"];
    opt.ClientSecret = builder.Configuration["GoogleClientSecret"];
});





/*    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new()
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"])),
                   ClockSkew = TimeSpan.FromHours(1),
               };
           });*/

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.WithOrigins()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
