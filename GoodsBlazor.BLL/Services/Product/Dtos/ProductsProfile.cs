using AutoMapper;
using GoodsBlazor.BLL.Services.Product.Commands.CreateProduct;
using GoodsBlazor.BLL.Services.Product.Commands.Update;

namespace GoodsBlazor.BLL.Services.Product.Dtos;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<GoodsBlazor.DAL.Entities.Product, GoodsBlazor.Shared.Dtos.ProductDto >()
            .ForMember(dest => dest.ImageBase64,
                opt => opt.MapFrom(src => src.ImageData != null ? Convert.ToBase64String(src.ImageData) : null))
                .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType != null ? src.ProductType.TypeName : null));

        CreateMap<GoodsBlazor.Shared.Dtos.ProductDto, GoodsBlazor.DAL.Entities.Product>()
            .ForMember(dest => dest.ImageData,
                opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ImageBase64) ? Convert.FromBase64String(src.ImageBase64) : null));

        CreateMap<CreateProductCommand, GoodsBlazor.DAL.Entities.Product>();
        CreateMap<UpdateProductCommand, GoodsBlazor.DAL.Entities.Product>();
    }
}
