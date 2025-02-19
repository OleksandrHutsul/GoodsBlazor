using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.BLL.Services.User.Commands.Login;
using GoodsBlazor.BLL.Services.User.Dtos;
using GoodsBlazor.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/cookie")]
[ApiController]
public class CookieAuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public CookieAuthController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand model)
    {
        var user = await _userRepository.GetByEmailAsync(model.Email);
        if (user is null)
        {
            return Unauthorized(new { message = "Incorrect email or password" });
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if (result != PasswordVerificationResult.Success)
        {
            return Unauthorized(new { message = "Incorrect email or password" });
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddDays(7)
        };

        var schemes = await HttpContext.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>().GetAllSchemesAsync();
        Console.WriteLine(string.Join(", ", schemes.Select(s => s.Name)));

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return Ok(new { message = "Successful login" });
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { message = "Вихід успішний" });
    }

    [HttpGet("user")]
    public IActionResult GetUser()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        return Ok(new UserDto
        {
            Id = int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0,
            Email = User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty
        });
    }
}