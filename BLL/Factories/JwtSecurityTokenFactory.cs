using BLL.Configurations;
using BLL.Factories.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Factories
{
    public class JwtSecurityTokenFactory : IJwtSecurityTokenFactory
    {
        private readonly JwtTokenConfiguration jwtTokenConfiguration;
        private readonly UserManager<User> userManager; 
        public JwtSecurityToken BuildToken(User user) => new(
            issuer: jwtTokenConfiguration.Issuer,
            audience: jwtTokenConfiguration.Audience,
            claims: GetClaims(user),
            expires: JwtTokenConfiguration.ExpirationDate,
            signingCredentials: jwtTokenConfiguration.Credentials);

        private  List<Claim> GetClaims(User user) => new()
        {
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Authentication, user.UserName),
            new(ClaimTypes.Role,GetRole(user))

        };

        private string GetRole(User user)
        {
            return userManager.GetRolesAsync(user).Result.First();



        }

        public JwtSecurityTokenFactory(JwtTokenConfiguration jwtTokenConfiguration, UserManager<User> userManager)
        {
            this.jwtTokenConfiguration = jwtTokenConfiguration;
            this.userManager = userManager;
        }
    }
}
