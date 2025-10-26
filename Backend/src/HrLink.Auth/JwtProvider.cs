using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HrLink.Application.DTOs;
using HrLink.Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HrLink.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly IOptions<JwtBearerOptions> _options;

    public JwtProvider(IOptions<JwtBearerOptions> options)
    {
        _options = options;
    }
    
    public string GenerateToken(LoginDataResponse data)
    {
        var claims = new List<Claim>()
        {
            new Claim("UserId", data.Id.ToString())
        };
        
        claims.AddRange(data.Roles.Select(x=> new Claim(ClaimTypes.Role, x.RoleName)).ToList());

        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey)),
                SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            issuer: _options.Value.Issuer,
            audience: _options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_options.Value.ExpiresHours),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}