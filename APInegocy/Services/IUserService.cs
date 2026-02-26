using APInegocy.DTOs;
using APInegocy.Models;

namespace APInegocy.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(UserCreateDto dto);
        Task<User?> UpdateAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<User?> LoginAsync(LoginDto dto);
    }
}