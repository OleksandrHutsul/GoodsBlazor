using GoodsBlazor.BLL.Services.CartItemRepository.Commands.AddToCart;
using GoodsBlazor.BLL.Services.Product.Queries.GetAllProducts;
using GoodsBlazor.Shared.Dtos;

namespace GoodsBlazor.Client.Pages;

public partial class Product
{
    private List<ProductDto>? products;
    private int userId;

    protected override async Task OnInitializedAsync()
    {
        userId = UserService.GetUserId(); 
        products = (await Mediator.Send(new GetAllProductsQuery())).ToList();
    }

    private async Task AddToCart(int productId)
    {
        if (userId > 0)
        {
            await Mediator.Send(new AddToCartCommand { UserId = userId, ProductId = productId });
        }
        else
        {
            Console.WriteLine("Користувач не авторизований!");
        }
    }
}
