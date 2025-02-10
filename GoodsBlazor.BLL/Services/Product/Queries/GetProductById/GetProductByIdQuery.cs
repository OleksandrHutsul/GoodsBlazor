using GoodsBlazor.BLL.Services.Product.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Queries.GetProductById;

public class GetProductByIdQuery(int id) : IRequest<ProductDto>
{
    public int Id { get; set; } = id;
}
