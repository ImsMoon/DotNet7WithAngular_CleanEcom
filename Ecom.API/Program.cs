using Ecom.API;
using Ecom.Domain.Entities.Identity;
using Ecom.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureService().Configure();

// using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// var userManager = services.GetRequiredService<UserManager<AppUser>>();
// await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

app.Run();

