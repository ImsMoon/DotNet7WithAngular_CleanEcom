using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Application.Helper.DTOs;
using Ecom.Application.ServiceContacts;
using Ecom.Domain.Entities.Identity;
using Ecom.Infrastructure.Helper;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

 
        public async Task<bool> CheckEmailExistsAsync(string? email)
        {
            return await _userManager.FindByEmailAsync(email!) != null;
        }

        public async Task<UserDto> GetCurrentUser(ClaimsPrincipal User)
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            if(user == null) return new UserDto();

            return new UserDto
            {
                Email=user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        public async Task<AddressDto> GetUserAddress(ClaimsPrincipal User)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
            return _mapper.Map<Address,AddressDto>(user.Address!);
        }

        public async Task<UserDto> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email!);
            if(user == null) return new UserDto();
            var result = await _signInManager.CheckPasswordSignInAsync(user,login.Password!,false);
            if(!result.Succeeded) return new UserDto();
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        public async Task<UserDto> Register(RegisterDto register)
        {
            var user = new AppUser
            {
                DisplayName = register.DisplayName,
                Email = register.Email,
                UserName = register.Email
            };

            var result = await _userManager.CreateAsync(user,register.Password!);

            if(!result.Succeeded)
            {
                return new UserDto();
            }

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        public async Task<AddressDto> UpdateUserAddress(ClaimsPrincipal User,AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

            user.Address = _mapper.Map<AddressDto,Address>(address);
            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                return new AddressDto();
            }

            return _mapper.Map<AddressDto>(result);

        }
    }
}