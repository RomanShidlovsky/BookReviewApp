using System.Reflection;
using Book.API.Extensions;
using Book.Application;
using Book.Infrastructure;
using Book.Infrastructure.Context;
using Book.Infrastructure.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.ApplyMigrations<BookContext>();
app.AddSeedData();

app.Run();