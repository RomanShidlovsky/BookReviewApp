using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;
using Ocelot.ApiGateway.Extensions;
using Ocelot.Middleware;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);
builder.Services.ConfigureLogging(builder, Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");
await app.UseAuthentication().UseOcelot();
app.UseAuthorization();

app.Run();