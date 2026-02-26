using APInegocy.DTOs;
using APInegocy.Models;
using APInegocy.Repository;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

public class ServiceManager<T> : IServiceManager<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public ServiceManager(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAll()
        => await _repository.GetAll();

    public async Task<T?> GetById(int id)
        => await _repository.GetById(id);

    public async Task<T> Create(T entity)
        => await _repository.Add(entity);

    public async Task<T> Update(T entity)
        => await _repository.Update(entity);

    public async Task<bool> Delete(int id)
        => await _repository.Delete(id);

    public async Task<User?> LoginAsync(LoginDto dto)
    {
        var user = await _repository.GetByUsernameAsync(dto.Username);

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