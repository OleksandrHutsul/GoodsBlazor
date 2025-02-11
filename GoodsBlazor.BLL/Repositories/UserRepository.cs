using GoodsBlazor.BLL.Interfaces;
using GoodsBlazor.DAL.Context;
using GoodsBlazor.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsBlazor.BLL.Repositories;

public class UserRepository(GoodsDbContext dbContext) : IUserRepository
{
    public async Task<int> Create(User entity)
    {
        dbContext.Users.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }
}