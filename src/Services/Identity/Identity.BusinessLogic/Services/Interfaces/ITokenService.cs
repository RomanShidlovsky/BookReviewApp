﻿using System.IdentityModel.Tokens.Jwt;
using Identity.DataAccess.Entities;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.BusinessLogic.Services.Interfaces;

public interface ITokenService
{
    public JwtSecurityToken GetJwtToken(User user, IEnumerable<string>? roles = null);
    public string GetRefreshToken();
    public Task<TokenValidationResult> ValidateTokenAsync(string token);
}