using Chat.Commons.Models;
using Chat.Library;
using Chat.Library.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(opts =>
{
    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddAuthentication()
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
            builder.Configuration.GetValue<string>("Authentication:SecretKey")!))
        };
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddData();
builder.Services.AddCore();

var app = builder.Build();

app.UseHttpsRedirection();


app.MapPost("/register", [AllowAnonymous] async ([FromBody] CreateUserRequest request, IUserRepository userRepository) =>
{
    Console.WriteLine("/register");

    if (await userRepository.CheckExistence(request.Email))
        return Results.Conflict("User with given email already exists.");

    var newUser = new User(request.Email, request.FirstName, request.LastName, SecretHasher.Hash(request.Password));
    newUser.UserId = await userRepository.Create(newUser);

    return newUser.UserId == 0 ? Results.Problem($"Rejestracja u¿ytkownika { newUser.Email } nie powiod³a siê.") : Results.Ok(newUser);
});

app.MapPost("/authenticate", [AllowAnonymous] async ([FromBody] AuthRequest request, IUserRepository userRepository, IJwtUtils jwtUtils) =>
{
    Console.WriteLine("/authenticate");

    var user = await userRepository.GetByEmail(request.Email);
    if(user is null) return Results.Unauthorized();

    bool passwordMatches = SecretHasher.Verify(request.Password, user.Password);
    if(!passwordMatches) return Results.Unauthorized();

    var token = jwtUtils.GenerateToken(user);
    var response = new AuthResponse(user, token);

    return Results.Ok(response);
});

app.MapGet("/test", () =>
{
    return "you are authorized";
});

app.Run();
