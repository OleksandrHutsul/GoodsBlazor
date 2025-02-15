using GoodsBlazor.BLL.Services.CartItemRepository.Commands.RemoveFromCart;
using GoodsBlazor.BLL.Services.CartItemRepository.Dtos;
using GoodsBlazor.BLL.Services.CartItemRepository.Queries.GetUserCart;

namespace GoodsBlazor.Client.Pages;

public partial class Cart
{
    private List<CartItemDto>? cartItems;
    private int userId;

    protected override async Task OnInitializedAsync()
    {
        userId = UserService.GetUserId();
        await LoadCart();
    }

    private async Task LoadCart()
    {
        if (userId > 0)
        {
            var items = await Mediator.Send(new GetUserCartQuery { UserId = userId });
            cartItems = items.GroupBy(item => item.ProductId).Select(group => group.First()).ToList();
        }
        else
        {
            Console.WriteLine("Користувач не авторизований!");
        }
    }

    private async Task RemoveFromCart(int productId)
    {
        if (userId > 0)
        {
            await Mediator.Send(new RemoveFromCartCommand { UserId = userId, ProductId = productId });
            cartItems = cartItems!.Where(item => item.ProductId != productId).ToList();
        }
    }
}
