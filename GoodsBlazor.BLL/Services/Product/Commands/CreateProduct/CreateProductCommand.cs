using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Commands.CreateProduct;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageBase64 { get; set; }
    public int ProductTypeId { get; set; } 
}