using AutoMapper;
using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.Shared.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IMapper mapper,
    IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync();
        return mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
