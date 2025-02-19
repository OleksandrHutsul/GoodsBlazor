using AutoMapper;
using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.Shared.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.ProductType.Commands.Queries;

public class GetAllProductsTypeQueryHandler(IMapper mapper,
    IProductRepository productRepository) : IRequestHandler<GetAllProductsTypeQuery, IEnumerable<ProductTypeDto>>
{
    public async Task<IEnumerable<ProductTypeDto>> Handle(GetAllProductsTypeQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllProductTypeAsync();
        return mapper.Map<IEnumerable<ProductTypeDto>>(products);
    }
}