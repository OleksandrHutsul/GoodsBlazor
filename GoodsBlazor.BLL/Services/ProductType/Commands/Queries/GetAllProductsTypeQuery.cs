using GoodsBlazor.Shared.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.ProductType.Commands.Queries;

public class GetAllProductsTypeQuery : IRequest<IEnumerable<ProductTypeDto>>
{
}
