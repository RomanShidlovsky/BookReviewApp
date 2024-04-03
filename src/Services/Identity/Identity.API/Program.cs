using System.Reflection;
using Identity.API.Extensions;
using Identity.BusinessLogic;
using Identity.DataAccess;
using Identity.DataAccess.Contexts;
using Identity.DataAccess.Seed;
using Microsoft.AspNetCore.HttpOverrides;
using Shared.Extensions;
using Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDataAccess(builder.Configuration);
builder.Services.ConfigureApi(builder.Configuration);
builder.Services.ConfigureBusinessLogic();

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
app.UseIdentityServer();
app.MapControllers();
app.ApplyMigrations<IdentityContext>();

var seedInitializer = app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedInitializer>();
await seedInitializer.Init();

app.Run();