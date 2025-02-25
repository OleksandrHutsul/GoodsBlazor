using GoodsBlazor.Admin;
using GoodsBlazor.Common.Services.Auth;
using GoodsBlazor.Common.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using GoodsBlazor.Common.Services.GetToken;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<TokenProvider>();

builder.Services.AddScoped(sp =>
{
    var tokenService = sp.GetRequiredService<TokenProvider>();
    var client = new HttpClient { BaseAddress = new Uri("https://localhost:7200/") };

    ConfigureHttpClientAsync(client, tokenService);

    return client;
});

async Task ConfigureHttpClientAsync(HttpClient client, TokenProvider tokenService)
{
    var token = await tokenService.GetTokenAsync();
    if (!string.IsNullOrWhiteSpace(token))
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}

await builder.Build().RunAsync();