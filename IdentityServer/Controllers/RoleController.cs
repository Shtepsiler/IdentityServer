using BLL.DTO.Requests;
using BLL.DTO.Responses;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Exceptions;
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



        [HttpPost]
        public IActionResult AsingRole(Guid id, string role)
        {

            try
            {
                _RoleService.AsignRole(id, role);
                return Ok();

            }
            catch (Exception ms)
            {
                return BadRequest(ms);

            }

        }

        /*

                //GET: api/jobs/Id
                [Authorize]
                [HttpGet("{Id}")]
                public async Task<ActionResult<UserResponse>> GetByIdAsync(Guid Id)
                {
                    try
                    {
                        var result = await userService.GetClientById(Id);

                        if (result == null)
                        {
                            _logger.LogInformation($"Юзер із Id: {Id}, не був знайдейний у базі даних");
                            return NotFound();
                        }
                        else
                        {
                            _logger.LogInformation($"Отримали івент з бази даних!");
                            return Ok(result);
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetByNameAsync() - {ex.Message}");
                        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                    }
                }




                //POST: api/jobs/Id
                [Authorize]
                [HttpPut("{Id}")]
                public async Task<ActionResult> UpdateAsync(Guid Id, [FromBody] UserRequest client)
                {
                    try
                    {
                        if (client == null)
                        {
                            _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                            return BadRequest("Обєкт  є null");
                        }
                        if (!ModelState.IsValid)
                        {
                            _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                            return BadRequest("Обєкт  є некоректним");
                        }
                        if (Id != client.Id)
                        {
                            _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                            return BadRequest("Обєкт  є некоректним");
                        }

                        await userService.UpdateAsync(Id, client);
                        return StatusCode(StatusCodes.Status204NoContent);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі UpdateAsync - {ex.Message}");
                        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                    }
                }

                //GET: api/jobs/Id
                [Authorize]
                [HttpDelete("{Id}")]
                public async Task<ActionResult> DeleteByIdAsync(Guid Id)
                {
                    try
                    {
                        var client = await userService.GetClientById(Id);
                        if (client == null)
                        {
                            _logger.LogInformation($"Запис із Id: {Id}, не був знайдейний у базі даних");
                            return NotFound();
                        }

                        await userService.DeleteAsync(client.Id);
                        return NoContent();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteByNameAsync() - {ex.Message}");
                        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                    }
                }


                [HttpPost("ResetPassword")]
                public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordRequest request)
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));
                    try
                    {
                        await userService.ResetPassword(request);
                        return Ok();
                    }
                    catch (EntityNotFoundException e)
                    {
                        return NotFound(new { e.Message });
                    }
                    catch (ArgumentException e)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
                    }
                    catch (Exception e)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
                    }
                }



                [HttpPost("ForgotPassword")]
                public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

                    try
                    {
                        await userService.ForgotPassword(request);
                        return Ok();
                    }
                    catch (EntityNotFoundException e)
                    {
                        return NotFound(new { e.Message });
                    }
                    catch (ArgumentException e)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
                    }
                    catch (Exception e)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
                    }
                }

            }
        */



    }
    }

