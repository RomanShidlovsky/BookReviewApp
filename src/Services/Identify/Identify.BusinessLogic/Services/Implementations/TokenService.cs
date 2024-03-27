using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identify.BusinessLogic.Models;
using Identify.BusinessLogic.Services.Interfaces;
using Identify.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Identify.BusinessLogic.Services.Implementations;

public class TokenService(JwtOptions jwtOptions) : ITokenService
{
    public JwtSecurityToken GetJwtToken(User user, IEnumerable<string>? roles = null)
    {
        var claims = new List<Claim>()
        {
            new("Id", user.Id.ToString()),
            new("UserName", user.UserName),
        };

        if (roles != null) 
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            jwtOptions.Issuer,
            null,
            claims,
            null,
            DateTime.UtcNow.AddMinutes(jwtOptions.TokenValidityInMinutes),
            signingCredentials);

        return token;
    }

    public string GetRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    public async Task<TokenValidationResult> ValidateTokenAsync(string token)
    {
        var validationParameters = jwtOptions.ValidationParameters;
        validationParameters.ValidateLifetime = false;

        var tokenHandler = new JwtSecurityTokenHandler();
        return await tokenHandler.ValidateTokenAsync(token, validationParameters);
    }
}