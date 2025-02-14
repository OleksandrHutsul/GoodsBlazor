using GoodsBlazor.Shared.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Queries.GetAllProducts;

public class GetAllProductsQuery: IRequest<IEnumerable<ProductDto>>
{
}
