using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GoodsBlazor.BLL.Services.User.Commands.Login;

public class LoginCommandHandle(IUserRepository userRepository,
    IPasswordHasher<GoodsBlazor.DAL.Entities.User> passwordHasher,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<LoginCommand, AuthResultDto>
{
    public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user is null || passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var accessToken = GenerateJwtToken(user, out DateTime expiration);
        var refreshToken = GenerateRefreshToken();

        var response = httpContextAccessor.HttpContext!.Response;
        response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return new AuthResultDto
        {
            AccessToken = accessToken,
            Expiration = expiration
        };
    }

    private string GenerateJwtToken(GoodsBlazor.DAL.Entities.User user, out DateTime expiration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        expiration = DateTime.UtcNow.AddMinutes(30);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: creds
        );

        expiration = token.ValidTo;
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}