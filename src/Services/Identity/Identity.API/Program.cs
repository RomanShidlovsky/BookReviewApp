using Identity.API.Extensions;
using Identity.BusinessLogic;
using Identity.DataAccess;
using Identity.DataAccess.Contexts;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Seed;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDataAccess(builder.Configuration);
builder.Services.ConfigureApi(builder.Configuration);
builder.Services.ConfigureBusinessLogic();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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