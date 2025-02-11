using GoodsBlazor.BLL.Services.User.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.User.Queries.GetUserById;

public class GetUserByIdQuery(int id) : IRequest<UserDto>
{
    public int Id { get; set; } = id;
}
