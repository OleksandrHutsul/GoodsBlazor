namespace GoodsBlazor.Client.Cookie;

public class CookieAuthService
{
    private readonly HttpClient _httpClient;

    public CookieAuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/cookie/login")
        {
            Content = JsonContent.Create(new { Email = email, Password = password })
        };

        request.Headers.Add("Cookie", "AuthCookie"); 

        var response = await _httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }

    public async Task LogoutAsync()
    {
        await _httpClient.PostAsync("api/cookie/logout", null);
    }
}
