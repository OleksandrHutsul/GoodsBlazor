namespace GoodsBlazor.Shared.Models;

public class ImageUploadResult
{
    public string ObjectUrl { get; set; } = string.Empty;
    public List<byte> ByteArray { get; set; } = new();
}