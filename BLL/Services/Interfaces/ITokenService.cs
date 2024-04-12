using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITokenService
    {
        string SerializeToken(JwtSecurityToken jwtToken);
        //bool IsValid(JwtResponse response, out string username);
        JwtSecurityToken BuildToken(User client);

    }
}
