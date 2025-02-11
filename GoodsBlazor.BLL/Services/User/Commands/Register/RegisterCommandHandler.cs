using AutoMapper;
using GoodsBlazor.BLL.Exceptions;
using GoodsBlazor.BLL.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GoodsBlazor.BLL.Services.User.Commands.Register;

public class RegisterCommandHandler(IMapper mapper,
    IUserRepository userRepository,
    IPasswordHasher<GoodsBlazor.DAL.Entities.User> passwordHasher) : IRequestHandler<RegisterCommand, int>
{
    public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            throw new ConflictException($"User with email {request.Email} already exists.");
        }

        var user = mapper.Map<GoodsBlazor.DAL.Entities.User>(request);
        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);
        user.Role = DAL.Entities.Role.User;

        int id = await userRepository.Create(user);
        return id;
    }
}
