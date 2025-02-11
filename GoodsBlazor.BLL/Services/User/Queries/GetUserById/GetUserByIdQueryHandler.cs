using AutoMapper;
using GoodsBlazor.BLL.Exceptions;
using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.BLL.Services.User.Dtos;
using MediatR;

namespace GoodsBlazor.BLL.Services.User.Queries.GetUserById;

public class GetUserByIdQueryHandler(IMapper mapper,
    IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(DAL.Entities.User), request.Id.ToString());

        var userDto = mapper.Map<UserDto>(user);

        return userDto;
    }
}
