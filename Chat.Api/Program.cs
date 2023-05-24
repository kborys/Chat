using Chat.Commons.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(opts =>
{
    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
            builder.Configuration.GetValue<string>("Authentication:SecretKey")))
        };
    });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseHttpsRedirection();


app.MapPost("/register", [AllowAnonymous] () =>
{
    Console.WriteLine("/register");
    return "register";
});

app.MapPost("/authenticate", [AllowAnonymous] (AuthRequest request) =>
{
    Console.WriteLine("/authenticate");

    return "register";
});

app.MapGet("/test", () =>
{
    return "you are authorized";
});

app.Run();
