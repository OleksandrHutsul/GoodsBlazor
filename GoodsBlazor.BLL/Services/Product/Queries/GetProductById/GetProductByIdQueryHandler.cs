using AutoMapper;
using GoodsBlazor.BLL.Exceptions;
using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.Shared.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Queries.GetProductById;

public class GetProductByIdQueryHandler(IMapper mapper,
    IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(GoodsBlazor.DAL.Entities.Product), request.Id.ToString());

        var productDto = mapper.Map<ProductDto>(product);

        return productDto;
    }
}
