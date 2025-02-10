using AutoMapper;
using GoodsBlazor.BLL.Services.Product.Commands.CreateProduct;
using GoodsBlazor.BLL.Services.Product.Commands.Update;

namespace GoodsBlazor.BLL.Services.Product.Dtos;

public class ProductsProfile: Profile
{
    public ProductsProfile()
    {
        CreateMap<GoodsBlazor.DAL.Entities.Product, ProductDto>();
        CreateMap<CreateProductCommand, GoodsBlazor.DAL.Entities.Product>();
        CreateMap<UpdateProductCommand, GoodsBlazor.DAL.Entities.Product>();
    }
}
