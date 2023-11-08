
using Microsoft.OpenApi.Models;
using Ecom.Infrastructure;

namespace Ecom.API;

public static class Startup
{
    public static WebApplication ConfigureService(this WebApplicationBuilder builder){
        AddSwagger(builder.Services);
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddCors(options=>{
            options.AddPolicy("Open",builder=>builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
        });
        return builder.Build();
    }

    public static WebApplication Configure(this WebApplication app)
    {
        if(app.Environment.IsDevelopment()){
            app.UseSwagger();
            app.UseSwaggerUI(s=>{
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Ecom API");
            });
        }
        app.UseAuthentication();
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("Open");
        app.MapControllers();
        return app;
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c=>{
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Clean Ecom"        
            });
        });
    }

}