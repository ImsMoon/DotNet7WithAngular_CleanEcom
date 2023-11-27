using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.API.Exceptions;
using Ecom.Application.Helper.DTOs;
using Ecom.Application.ServiceContacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseAPIController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(await _authService.GetCurrentUser(User));
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _authService.Login(login);
            if(result == null)
                return Unauthorized(new ApiResponse(401));
            return Ok(result);
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            if(await _authService.CheckEmailExistsAsync(register.Email))
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                        {Errors = new [] {"Email address is in use"}});
            }

            var result = await _authService.Register(register);

            if(result ==  null)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Created("",result);
        }

        [HttpGet("address")]
        public async Task<IActionResult> GetUserAddress()
        {
            return Ok(await _authService.GetUserAddress(User));
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<IActionResult> UpdateUserAddress(AddressDto address)
        {
            var result = await _authService.UpdateUserAddress(User,address);

            if(result == null)
            {
                return BadRequest("Problem updating the user");
            }

            return Ok(result);
        }
    }
}