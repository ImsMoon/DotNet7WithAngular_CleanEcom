using Ecom.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureService().Configure();

// using var scope = app.Services.CreateScope();
// var services = scope.ServiceProvider;
// var userManager = services.GetRequiredService<UserManager<AppUser>>();
// await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

app.Run();

