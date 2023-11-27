using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.ServiceContacts;
using Ecom.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Services
{
    public class IdentityDbService : IIdentityDbService
    {
        private readonly UserManager<AppUser> _userManager;

        public IdentityDbService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ApplyMigrationsAsync()
        {
            await AppIdentityDbContextSeed.SeedUsersAsync(_userManager);
        }
    }
}