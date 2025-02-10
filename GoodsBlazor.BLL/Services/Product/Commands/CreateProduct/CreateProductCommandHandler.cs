using AutoMapper;
using GoodsBlazor.BLL.Interfaces;
using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Commands.CreateProduct;

public class CreateProductCommandHandler(IMapper mapper,
    IProductRepository productRepository) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<GoodsBlazor.DAL.Entities.Product>(request);

        int id = await productRepository.Create(product);

        return id;
    }
}