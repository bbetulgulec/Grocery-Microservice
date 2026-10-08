using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace manav.identityServer.Tools;

public class JwtTokenGenerator
{
    public static TokenResponseViewModel GenerateToken(
        GetCheckAppUserViewModel model,
        IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"]
                     ?? throw new InvalidOperationException("Jwt:Key ayarı bulunamadı.");

        var claims = new List<Claim>();

        if (!string.IsNullOrWhiteSpace(model.Role))
            claims.Add(new Claim(ClaimTypes.Role, model.Role));

        claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id));

        if (!string.IsNullOrWhiteSpace(model.Username))
            claims.Add(new Claim("Username", model.Username));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var signingCredentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefault.Expire);

        var token = new JwtSecurityToken(
            JwtTokenDefault.ValidIssuer,
            JwtTokenDefault.ValidAudience,
            claims,
            DateTime.UtcNow,
            expireDate,
            signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        return new TokenResponseViewModel(
            tokenHandler.WriteToken(token),
            expireDate);
    }
}