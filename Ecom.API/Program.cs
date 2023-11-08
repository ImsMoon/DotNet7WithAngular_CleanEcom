using Ecom.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureService().Configure();

app.Run();

