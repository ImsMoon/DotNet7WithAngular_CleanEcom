using Ecom.API.Exceptions;
using Ecom.Application.Helper.DTOs;
using Ecom.Application.ServiceContacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : BaseAPIController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _authService.GetCurrentUser(User));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto login)
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

        [Authorize]
        [HttpGet("address")]
        public async Task<IActionResult> GetUserAddress()
        {           
            return Ok(await _authService.GetUserAddress(User));
        }

        [Authorize]
        [HttpPut("address")]
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