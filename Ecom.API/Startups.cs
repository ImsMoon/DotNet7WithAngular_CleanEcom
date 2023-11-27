
using Microsoft.OpenApi.Models;
using Ecom.Infrastructure;
using Ecom.Application;
using Ecom.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Ecom.API;

public static class Startup
{
    public static WebApplication ConfigureService(this WebApplicationBuilder builder){
        AddSwagger(builder.Services);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        // retunr custom error page if url is incorrect
        builder.Services.Configure<ApiBehaviorOptions>(options=>{
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                .Where(e=>e.Value!.Errors.Count > 0)
                .SelectMany(x=>x.Value!.Errors)
                .Select(x=>x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        //add cors
        builder.Services.AddCors();

        //serilog        
        builder.Host.UseSerilog((context, loggerConfig)=>
            loggerConfig.WriteTo.Console().ReadFrom.Configuration(context.Configuration),true);

        return builder.Build();
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.UseStatusCodePagesWithReExecute("errors/{code}");
        app.UseMiddleware<ExceptionMiddleware>();
        if(app.Environment.IsDevelopment()){
            app.UseSwagger();
            app.UseSwaggerUI(s=>{
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Ecom API");
            });
        }
        app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            .SetIsOriginAllowed(hostName => true));
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        //serilog
        app.UseSerilogRequestLogging();
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