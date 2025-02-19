using AutoMapper;

namespace GoodsBlazor.BLL.Services.ProductType.Commands.Dto;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        CreateMap<DAL.Entities.ProductType, Shared.Dtos.ProductTypeDto>();
    }
}
