using AutoMapper;
using GoodsBlazor.BLL.Exceptions;
using GoodsBlazor.BLL.Interfaces;
using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Commands.Update;

public class UpdateProductCommandHandler(IProductRepository productRepository, 
    IMapper mapper) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            throw new NotFoundException(nameof(GoodsBlazor.DAL.Entities.Product), request.Id.ToString());
        }

        mapper.Map(request, product);

        await productRepository.SaveChanges();
    }
}
