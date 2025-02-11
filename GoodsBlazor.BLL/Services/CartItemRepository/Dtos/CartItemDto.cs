namespace GoodsBlazor.BLL.Services.CartItemRepository.Dtos;

public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal Price { get; set; }
}