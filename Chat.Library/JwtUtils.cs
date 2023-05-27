using Chat.Commons.Contracts;
using Chat.Commons.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat.Library;

public class JwtUtils : IJwtUtils
{
    private readonly IConfiguration _config;

    public JwtUtils(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            _config["Authentication:SecretKey"]));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var expiresInMinutes = int.Parse(_config["Authentication:ExpiresInMinutes"]);
        var expirationDate = DateTime.UtcNow.AddMinutes(expiresInMinutes);
        var token = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                DateTime.UtcNow,
                expirationDate,
                signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}