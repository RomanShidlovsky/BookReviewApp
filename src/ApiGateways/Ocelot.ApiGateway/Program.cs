using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLogging(builder, Assembly.GetExecutingAssembly().GetName().Name!);

var app = builder.Build();

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

app.Run();