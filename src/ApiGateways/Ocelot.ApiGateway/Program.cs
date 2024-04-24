using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;
using Ocelot.ApiGateway.Extensions;
using Ocelot.Middleware;
using Shared.Extensions;
using Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);
builder.Services.ConfigureLogging(builder, Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.AddEndpointsApiExplorer();
//builder.Services.ConfigureSwagger(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

/*if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwaggerForOcelotUI(opt => opt.PathToSwaggerGenerator = "/swagger/docs");
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");
await app.UseAuthentication().UseOcelot();
app.UseAuthorization();

app.Run();