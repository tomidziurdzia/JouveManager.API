using System.Text;
using JouveManager.Application;
using JouveManager.Domain;
using JouveManager.Infrastructure;
using JouveManager.Infrastructure.Data;
using JouveManager.Infrastructure.Data.Extensions;
using JouveManager.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder
            .WithOrigins(
                "https://financestock.vercel.app",
                "http://localhost:3000"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Add services to the container.
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddWebApiServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders()
    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User>>();

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

// Configure the HTTP request pipeline.

app.UseApiServices();
app.RegisterUserEndpoints();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseAuthentication();
app.UseAuthorization();


app.Run();
