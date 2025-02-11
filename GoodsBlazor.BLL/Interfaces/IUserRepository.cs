using GoodsBlazor.DAL.Entities;

namespace GoodsBlazor.BLL.Interfaces;

public interface IUserRepository
{
    public Task<int> Create(User entity);
    public Task<User?> GetByEmailAsync(string email);
    public Task<User?> GetByIdAsync(int id);
}
