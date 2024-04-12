using BLL.DTO.Responses;
using BLL.Factories.Interfaces;
using BLL.Services.Interfaces;
using DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
     //   private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        private readonly IJwtSecurityTokenFactory tokenFactory;


        public TokenService( IJwtSecurityTokenFactory tokenFactory, IConfiguration configuration)
        {
          
            this.configuration = configuration;
            this.tokenFactory = tokenFactory;
        }
        public string SerializeToken(JwtSecurityToken jwtToken) =>
    new JwtSecurityTokenHandler().WriteToken(jwtToken);

        public JwtSecurityToken BuildToken(User client) => tokenFactory.BuildToken(client);



     

     
   /*     public bool IsValid(JwtResponse response, out string username)
        {
            username = string.Empty;
            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(response.Token);
            if (principal is null)
            {
                throw new UnauthorizedAccessException("No principal");
            }

            username = principal.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(username))
            {
                throw new UnauthorizedAccessException("No user name");
            }

            if (!Guid.TryParse(response.RefreshToken, out Guid givenRefreshToken))
            {
                throw new UnauthorizedAccessException("Refresh token malformed");
            }
            var curenttoken = unitOfWork._TokenRepository.GeTokenByClientName(response.ClientName);
            Guid curentRefreshToken = Guid.Parse(curenttoken.Result.ClientSecret);

            if (curenttoken.Result.ExpirationDate >= DateTime.Now)
            {
                unitOfWork._TokenRepository.DeleteTokenByClientName(username);
                unitOfWork.SaveChangesAsync();
                throw new UnauthorizedAccessException("Refresh Token is expired,it will be deleted");

            }

            if (curentRefreshToken == null)
            {
                throw new UnauthorizedAccessException("No valid refresh token in system");

            }
            if (curentRefreshToken != givenRefreshToken)
            {
                throw new UnauthorizedAccessException("invalid refresh token");
            }

            return true;

        }
        
  
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                                 Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"])),



            };
            
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            ClaimsPrincipal claimsPrincipal = handler.ValidateToken(
                token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("invalid token");
            }


            return claimsPrincipal;

        }*/
        






    }
}
