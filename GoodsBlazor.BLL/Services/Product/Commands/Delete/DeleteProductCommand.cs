using MediatR;

namespace GoodsBlazor.BLL.Services.Product.Commands.Delete;

public class DeleteProductCommand(int id): IRequest 
{
    public int Id { get; } = id;
}