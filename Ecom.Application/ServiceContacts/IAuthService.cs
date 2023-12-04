using System.Security.Claims;
using Ecom.Application.Helper.DTOs;

namespace Ecom.Application.ServiceContacts
{
    public interface IAuthService
    {
        Task<UserDto> GetCurrentUser(ClaimsPrincipal User);
        Task<UserDto> Login(LoginDto login);
        Task<UserDto> Register(RegisterDto register);
        Task<bool> CheckEmailExistsAsync(string? email);
        Task<AddressDto> GetUserAddress(ClaimsPrincipal User);
        Task<AddressDto> UpdateUserAddress(ClaimsPrincipal User,AddressDto address);
    }
}