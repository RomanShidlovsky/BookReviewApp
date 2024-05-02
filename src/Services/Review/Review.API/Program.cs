using System.Reflection;
using Microsoft.AspNetCore.HttpOverrides;
using Review.API.Extensions;
using Review.Application;
using Review.Application.GrpcServices;
using Review.Infrastructure;
using Review.Infrastructure.Context;
using Review.Infrastructure.Extensions;
using Shared.Extensions;
using Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication();
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

app.MapGrpcService<GrpcReviewService>();
app.MapControllers();

app.AddSeedData();

app.Run();