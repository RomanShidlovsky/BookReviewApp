using System.Reflection;
using Book.API.Extensions;
using Book.Application;
using Book.Infrastructure;
using Book.Infrastructure.Context;
using Book.Infrastructure.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using Shared.Extensions;
using Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureApi(builder.Configuration);

builder.Services.ConfigureLogging(builder, Assembly.GetExecutingAssembly().GetName().Name!);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");


var resourcePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");

// Check if the directory exists, if not, create it
if (!Directory.Exists(resourcePath))
{
    Directory.CreateDirectory(resourcePath);
}

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(resourcePath),
    RequestPath = new PathString("/Resources")
});


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.ApplyMigrations<BookContext>();
app.AddSeedData();

app.Run();