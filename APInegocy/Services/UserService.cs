using APInegocy.DTOs;
using APInegocy.Models;
using APInegocy.Repository;
using System.Security.Cryptography;
using System.Text;

namespace APInegocy.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;

        public UserService(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<User> CreateAsync(UserCreateDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                Role = dto.Role
            };

            return await _repository.Add(user);
        }

        public async Task<User?> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _repository.GetById(id);

            if (user == null)
                return null;

            user.Username = dto.Username;
            user.Role = dto.Role;

            return await _repository.Update(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<User?> LoginAsync(LoginDto dto)
        {
            var users = await _repository.GetAll();
            var user = users.FirstOrDefault(u => u.Username == dto.Username);

            if (user == null)
                return null;

            var hashedInput = HashPassword(dto.Password);

            if (user.PasswordHash != hashedInput)
                return null;

            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}