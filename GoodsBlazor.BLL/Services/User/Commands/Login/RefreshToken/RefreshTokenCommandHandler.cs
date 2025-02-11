using GoodsBlazor.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GoodsBlazor.BLL.Services.User.Commands.Login.RefreshToken;

public class RefreshTokenCommandHandler(IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration) : IRequestHandler<RefreshTokenCommand, AuthResultDto>
{
    public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var context = httpContextAccessor.HttpContext!;
        if (!context.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException("No refresh token provided");
        }

        var newAccessToken = GenerateJwtToken(out DateTime expiration);
        return new AuthResultDto
        {
            AccessToken = newAccessToken,
            Expiration = expiration
        };
    }

    private string GenerateJwtToken(out DateTime expiration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        expiration = DateTime.UtcNow.AddMinutes(30);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            null,
            expires: expiration,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}