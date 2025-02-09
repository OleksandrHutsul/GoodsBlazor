namespace GoodsBlazor.DAL.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } 
    public decimal Price { get; set; } = default!;
    public byte[]? ImageData { get; set; } 
}