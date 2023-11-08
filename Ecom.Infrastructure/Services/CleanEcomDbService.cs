using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.ServiceContacts;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Services
{
    public class CleanEcomDbService : ICleanEcomDbService
    {
        private readonly StoreDbContext _dbContext;

        public CleanEcomDbService(StoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> ApplyMigrationsAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            await _dbContext.Database.MigrateAsync();
            return pendingMigrations.Count();
        }
    }
}