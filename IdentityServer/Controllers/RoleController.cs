using BLL.DTO.Requests;
using BLL.DTO.Responses;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Exceptions;
using IdentityServer.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
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


        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRoleAsync(Guid id, string role)
        {

            try
            {
                await _RoleService.AssignRole(id, role);
                return Ok();

            }
            catch (Exception ms)
            {
                return BadRequest(ms);

            }

        }


        [HttpPost("UnAssignRole")]
        public async Task<IActionResult> UnAssignRoleAsync(Guid id, string role)
        {

            try
            {
                await _RoleService.UnAssignRole(id, role);
                return Ok();

            }
            catch (Exception ms)
            {
                return BadRequest(ms);

            }

        }

        [HttpGet("GetRoles")]


        public async Task<IActionResult> GetRolesAsync()
        {

            try
            {
                return Ok(await _RoleService.GetRolesAsync());

            }
            catch (Exception ms)
            {
                return BadRequest(ms);

            }

        }


        [HttpGet("GetRolesForUser")]


        public async Task<IActionResult> GetRolesForUserAsync([FromQuery]Guid id)
        {

            try
            {

                return Ok(await _RoleService.GetRolesForUserAsync(id));

            }
            catch (Exception ms)
            {
                return BadRequest(ms);

            }

        }













    }
}

