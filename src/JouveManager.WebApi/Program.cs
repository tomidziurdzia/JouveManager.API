using System.Text;
using JouveManager.Application;
using JouveManager.Application.Models.Authorization;
using JouveManager.Infrastructure;
using JouveManager.Infrastructure.Data.Extensions;
using JouveManager.WebApi;
using JouveManager.WebApi.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jouveManagerUrl = builder.Configuration["JouveManagerUrl"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder
            .WithOrigins(jouveManagerUrl!)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddWebApiServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });


builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors("AllowSpecificOrigins");

// Configure the HTTP request pipeline.
app.UseApiServices();
app.RegisterUserEndpoints()
    .RegisterVehicleEndpoints()
    .RegisterSemiTrailerEndpoints()
    .RegisterShipmentEndpoints()
    .RegisterTravelEndpoints()
    .RegisterTravelShipmentEndpoints();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseAuthentication();
app.UseAuthorization();


app.Run();
