
using DAL.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace BLL.Factories.Interfaces
{
    public interface IJwtSecurityTokenFactory
    {
        JwtSecurityToken BuildToken(User User);
    }
}
