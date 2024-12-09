using Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositryContracts;
using Repositaries;
using ServiceContracts;
using Services;
using Serilog;
using CRUDExample.Filters.ActionFilters;
using CRUDExample;
using CRUDExample.StartupExtensions;
using CRUDExample.ExceptionMiddleware;
//adding services to DI the container
var builder = WebApplication.CreateBuilder(args);



// Registering Serilog  as Service(Logging framework )

builder.Host.UseSerilog((HostBuilderContext context,
	IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
	//Reading Configuration from Built-in IConfiguration 
	loggerConfiguration.ReadFrom.Configuration(context.Configuration)
	//services and make avilable them to serilog
	.ReadFrom.Services(services);
});




builder.Services.ConfigureServices(builder.Configuration) ;

var app = builder.Build();


//Enabling HttpLogging

app.UseHttpLogging();

         // Http Request Pipeline

if(builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Error"); // its a built in Exception Middleware that is executed for every request
	app.UseExceptionHandlingMiddleware();
} 

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot",wkhtmltopdfRelativePath: "Rotativa");

//app.Logger.LogDebug("Debug-message");
//app.Logger.LogInformation("Information-message");
//app.Logger.LogError("Error-message");
//app.Logger.LogWarning("Warning-message");
//app.Logger.LogCritical("Critical-message");

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();

