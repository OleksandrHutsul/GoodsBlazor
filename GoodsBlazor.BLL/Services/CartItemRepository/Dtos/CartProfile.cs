using AutoMapper;
using GoodsBlazor.BLL.Services.CartItemRepository.Commands.AddToCart;
using GoodsBlazor.BLL.Services.CartItemRepository.Commands.RemoveFromCart;
using GoodsBlazor.BLL.Services.User.Queries.GetUserById;
using GoodsBlazor.DAL.Entities;

namespace GoodsBlazor.BLL.Services.CartItemRepository.Dtos;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

        CreateMap<AddToCartCommand, CartItem>();
        CreateMap<RemoveFromCartCommand, CartItem>();
        CreateMap<GetUserByIdQuery, CartItem>();
    }
}