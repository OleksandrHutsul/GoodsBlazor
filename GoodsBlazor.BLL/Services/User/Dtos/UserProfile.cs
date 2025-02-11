using AutoMapper;
using GoodsBlazor.BLL.Services.User.Commands.Register;

namespace GoodsBlazor.BLL.Services.User.Dtos;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<GoodsBlazor.DAL.Entities.User, UserDto>();
        CreateMap<RegisterCommand, GoodsBlazor.DAL.Entities.User>();
    }
}
