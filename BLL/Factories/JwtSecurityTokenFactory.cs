using BLL.Configurations;
using BLL.Factories.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Factories
{
    public class JwtSecurityTokenFactory : IJwtSecurityTokenFactory
    {
        private readonly JwtTokenConfiguration jwtTokenConfiguration;
        private readonly UserManager<User> userManager;

        public JwtSecurityToken BuildToken(User user) => new JwtSecurityToken(
            issuer: jwtTokenConfiguration.Issuer,
            audience: jwtTokenConfiguration.Audience,
            claims: GetClaims(user),
            expires: jwtTokenConfiguration.ExpirationDate,
            signingCredentials: jwtTokenConfiguration.Credentials);

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Authentication, user.UserName)
            };

            // Add user roles as claims
            var roles = userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            } 

            return claims;
        }

        public JwtSecurityTokenFactory(JwtTokenConfiguration jwtTokenConfiguration, UserManager<User> userManager)
        {
            this.jwtTokenConfiguration = jwtTokenConfiguration;
            this.userManager = userManager;
        }
    }
}
