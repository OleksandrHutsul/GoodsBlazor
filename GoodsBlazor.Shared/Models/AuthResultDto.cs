namespace GoodsBlazor.Shared.Models;

public class AuthResultDto
{
    public string AccessToken { get; set; } = default!;
    public DateTime Expiration { get; set; }
}
