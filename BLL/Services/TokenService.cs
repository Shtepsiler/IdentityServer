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

    }
}
