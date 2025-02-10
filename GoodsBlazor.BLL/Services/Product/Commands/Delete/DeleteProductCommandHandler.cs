using GoodsBlazor.BLL.Exceptions;
using GoodsBlazor.BLL.Interfaces;
using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Commands.Delete;

public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            throw new NotFoundException(nameof(GoodsBlazor.DAL.Entities.Product), request.Id.ToString());
        }

        await productRepository.Delete(product);
    }
}
