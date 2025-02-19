using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.BLL.Repositories;
using GoodsBlazor.BLL.Services.Product.Queries.GetAllProducts;
using GoodsBlazor.Client.Cookie;
using GoodsBlazor.Client.Services;
using GoodsBlazor.Common.Services.Interfaces;
using GoodsBlazor.DAL.Entities;
using GoodsBlazor.DAL.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductsQueryHandler).Assembly));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthStateProvider>();
builder.Services.AddScoped<IUserService, GoodsBlazor.Common.Services.Register.UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

builder.Services.AddScoped(sp => new HttpClient(new HttpClientHandler
{
    UseCookies = true,
    CookieContainer = new CookieContainer(),
    AllowAutoRedirect = true,
    UseDefaultCredentials = true
})
{
    BaseAddress = new Uri("https://localhost:7200"), 
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<CookieAuthService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();