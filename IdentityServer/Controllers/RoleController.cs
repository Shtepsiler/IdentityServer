using BLL.DTO.Requests;
using BLL.DTO.Responses;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _RoleService;

        private readonly ILogger<RoleController> _logger;
        public RoleController(
            ILogger<RoleController> logger,
             IRoleService userService)
        {
            _logger = logger;
            this._RoleService = userService;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "admin")]
        [HttpPost("AsingRole")]
        public async Task<IActionResult> AsingRoleAsync(Guid id, string role)
        {

            try
            {
                await _RoleService.AsignRole(id, role);
                return Ok();

            }
            catch (Exception ms)
            {
                return BadRequest(ms);

            }

        }








      

    }
    }

